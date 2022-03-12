using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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



    [SerializeField] private bool firstStatue = false;
    [SerializeField] private bool secondStatue = false;
    [SerializeField] private bool thirdStatue = false;
    [SerializeField] private bool fourthStatue = false;

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

    }


}
