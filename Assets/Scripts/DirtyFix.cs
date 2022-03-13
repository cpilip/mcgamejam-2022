using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DirtyFix : MonoBehaviour
{
    public static bool yes = true;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (yes)
        {
            Debug.Log("CALLED");
            yes = false;
            this.gameObject.GetComponent<Image>().enabled = false;
            SceneManager.sceneLoaded += reset;
        }
    }


    private void reset(Scene scene, LoadSceneMode mode)
    {
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {

            this.gameObject.GetComponent<Image>().enabled = true;
            SceneManager.sceneLoaded -= reset;
        }
    }
}
