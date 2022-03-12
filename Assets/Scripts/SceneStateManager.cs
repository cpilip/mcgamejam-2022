using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum Dimension
{
    RED,
    GREEN,
    BLUE
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


    [SerializeField] private Dimension m_nextDimToLoad = Dimension.GREEN;

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
                m_nextDimToLoad = Dimension.GREEN;
                break;
            case 2:
                Debug.Log("Swapped to Blue.");
                m_nextDimToLoad = Dimension.RED;
                break;
            case 3:
                Debug.Log("Swapped to Green.");
                m_nextDimToLoad = Dimension.BLUE;

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
