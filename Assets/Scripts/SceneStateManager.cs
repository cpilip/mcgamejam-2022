using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

enum Dimension
{
    RED,
    GREEN,
    BLUE,
    RETURNTOBURROW
}

public class SceneStateManager : MonoBehaviour
{
    [SerializeField] private SceneTransitioner m_sceneTransitioner;
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

    public bool debugMode;
    public GameObject ctrlMenu, riddleMenu;
    private bool menuUp, gotRiddle;
    private GameObject menu;

    [SerializeField] private Dimension m_currentDim = Dimension.RED;
    [SerializeField] public static bool m_puzzleSolvedGreen = false;
    [SerializeField] public static bool m_puzzleSolvedRed = false;
    [SerializeField] public static bool m_puzzleSolvedBlue = false;
    private bool burrowTeleporter = false;
    public static bool[] statueStates = { false, true, false, true };
    public static bool[] wallStates = { false, false, false, false };
    public static bool[] lightStates = { false, false, false, false, false, false };

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        debugMode = false;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += InitializeSceneData;
    }

    void InitializeSceneData(Scene scene, LoadSceneMode mode)
    {

        m_sceneTransitioner.gameObject.SetActive(false);
        m_sceneTransitioner.gameObject.SetActive(true);
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

                interactables = GameObject.FindGameObjectsWithTag("State");
                Transform lights = interactables[0].transform.GetChild(0);

                foreach (Transform l in lights)
                {
                    l.gameObject.GetComponent<InteractableLight>().ResetState();
                }

                note = interactables[0].transform.GetChild(1);

                note.gameObject.GetComponent<InteractableNote>().ResetState();

                break;
            case 3:
                Debug.Log("Swapped to Green.");

                interactables = GameObject.FindGameObjectsWithTag("State");
                Transform buttons = interactables[0].transform.GetChild(1);

                foreach (Transform b in buttons)
                {
                    b.gameObject.GetComponent<InteractableSeason>().ResetState();
                }

                note = interactables[0].transform.GetChild(2);

                note.gameObject.GetComponent<InteractableNote>().ResetState();

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

    public void GetRiddle()
    {
        gotRiddle = true;
        ShowRiddle();
    }

    void ShowRiddle()
    {
        menuUp = true;
        menu = Instantiate(riddleMenu, FindObjectOfType<Canvas>().transform);
        switch (m_currentDim)
        {
            case Dimension.RED:
                menu.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "You can�t be caught, do not be killed\n"
                                                                                            + "Your blood must pump, must not be spilled.\n"
                                                                                            + "So keep your eyes upon the beast\n"
                                                                                            + "Who sees you there, an evening feast\n"
                                                                                            + "And pray the one, with kit in tow,\n"
                                                                                            + "She will not see the fangs below\n"
                                                                                            + "The blades of grass and gleaming eyes\n"
                                                                                            + "That stare and spell his sure demise.";
                break;

            case Dimension.GREEN:
                menu.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "The wind shall blow, the rain shall fall\n"
                                                                                            + "And to traverse this ancient hall\n"
                                                                                            + "The song of seasons you must sing,\n"
                                                                                            + "To hide among the verdant spring.\n"
                                                                                            + "To choose the one that does not match\n"
                                                                                            + "Shall send you tumbling down the hatch\n";
                break;

            case Dimension.BLUE:
                menu.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Dark cannot be seen, cannot be felt,\n"
                                                                                            + "Cannot be heard, cannot be smelt.\n"
                                                                                            + "The more you have, the less you�ll see.\n"
                                                                                            + "Here in the deep and dim black sea.\n"
                                                                                            + "We welcome night by twinkling bright,\n"
                                                                                            + "so catch us all to bring the light.";
                break;
            default:
                Debug.Log("No riddle found in scene");
                break;
        }
    }
    public void GoToNextDim()
    {
        Debug.Log("Bool is true.");
        burrowTeleporter = true;
    }

    void Update()
    {
        
        // Debug.Log(m_currentDim);
        if (burrowTeleporter)
        {
            Debug.Log(m_currentDim);
            burrowTeleporter = false;
            switch (m_currentDim)
            {
                case Dimension.RED:
                    Debug.Log("Current dim is red.");
                    Debug.Log(m_sceneTransitioner);
                    m_sceneTransitioner.FadeToLevel(1);
                    break;
                case Dimension.GREEN:
                    m_sceneTransitioner.FadeToLevel(3);
                    break;
                case Dimension.BLUE:
                    m_sceneTransitioner.FadeToLevel(2);
                    break;
                case Dimension.RETURNTOBURROW:
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            debugMode = !debugMode;     // toggle debug mode
        }
        if (debugMode)      // debug mode enables scene changes with keys
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                m_sceneTransitioner.FadeToLevel(1);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                m_sceneTransitioner.FadeToLevel(3);
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                m_sceneTransitioner.FadeToLevel(2);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                m_sceneTransitioner.FadeToLevel(4);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Input = C");
            if (!menuUp)
            {
                Debug.Log("Instantiating menu");
                menu = Instantiate(ctrlMenu, FindObjectOfType<Canvas>().transform);
                Debug.Log(menu);
                menuUp = true;
            }
            else if (menuUp)
            {
                Destroy(menu);
                menuUp = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!menuUp)
            {
                if (gotRiddle)
                {
                    ShowRiddle();
                }
            }
            else if (menuUp)
            {
                Destroy(menu);
                menuUp = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuUp)
            {
                Destroy(menu);
                menuUp = false;
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
