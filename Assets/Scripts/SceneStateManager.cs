using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

enum Dimension
{
    RED,
    GREEN,
    BLUE,
    RETURNTOBURROW
}

public class SceneStateManager : MonoBehaviour
{
    private static SceneStateManager instance = null;

    private SceneStateManager()
    {
    }

    public static SceneStateManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SceneStateManager();
            }
            return instance;
        }
    }


    [SerializeField] private Dimension m_currentDim = Dimension.RED;
    [SerializeField] public static bool m_puzzleSolvedGreen = false;
    [SerializeField] public static bool m_puzzleSolvedRed = false;
    [SerializeField] public static bool m_puzzleSolvedBlue = false;
    public static bool[] statueStates = { false, true, false, true };

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += InitializeSceneData;
    }

    void InitializeSceneData(Scene scene, LoadSceneMode mode)
    {
        
        switch (scene.buildIndex)
        {
            case 0:
                Debug.Log("Swapped to Main Menu.");
                break;
            case 1:
                Debug.Log("Swapped to Red.");
                GameObject[] interactables = GameObject.FindGameObjectsWithTag("State");
                Transform statues = interactables[0].transform.GetChild(0);

                foreach (Transform i in statues)
                {
                    i.gameObject.GetComponent<InteractableStatue>().ResetState();
                }
                Transform note = interactables[0].transform.GetChild(1);

                note.gameObject.GetComponent<InteractableNote>().ResetState();
                

                break;
            case 2:
                Debug.Log("Swapped to Blue.");
                break;
            case 3:
                Debug.Log("Swapped to Green.");

                break;
            case 4:
                Debug.Log("Swapped to Burrow.");

                
                

                break;
            case 5:
                Debug.Log("Swapped to Finale.");
                break;
            default:
                Debug.Log("Swapped to Test Scene.");
                break;

        }

        SetNextDimension();

    }

    void SetNextDimension()
    {
        switch (m_currentDim)
        {
            case Dimension.RED:
                
                if (!m_puzzleSolvedGreen)
                {
                    m_currentDim = Dimension.GREEN;
                } else if (!m_puzzleSolvedBlue)
                {
                    m_currentDim = Dimension.BLUE;
                }
                else
                {
                    m_currentDim = Dimension.RETURNTOBURROW;
                }

                break;

            case Dimension.GREEN:

                if (!m_puzzleSolvedBlue)
                {
                    m_currentDim = Dimension.BLUE;
                }
                else if (!m_puzzleSolvedRed)
                {
                    m_currentDim = Dimension.RED;
                }
                else
                {
                    m_currentDim = Dimension.RETURNTOBURROW;
                }
                
                break;

            case Dimension.BLUE:

                if (!m_puzzleSolvedRed)
                {
                    m_currentDim = Dimension.RED;
                }
                else if (!m_puzzleSolvedGreen)
                {
                    m_currentDim = Dimension.GREEN;
                }
                else
                {
                    m_currentDim = Dimension.RETURNTOBURROW;
                }

                break;
            default:
                Debug.Log("Swapped to Test Scene.");
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Red");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("Green");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("Blue");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("Burrow");
        }
    }
}
