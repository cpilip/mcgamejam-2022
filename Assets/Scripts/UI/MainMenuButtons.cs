using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject credits;
    private SoundManager smgr;

    private void Awake()
    {
        credits.SetActive(false);
        smgr = GameObject.FindWithTag("SFX").GetComponent<SoundManager>();
        smgr.StartFadeIn("musicMenu");
    }

    private void OnDisable()
    {
        smgr.StartFadeOut("musicMenu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Burrow");
    }
    public void OpenCredits()
    {
        credits.SetActive(true);
    }
    public void CloseCredits()
    {
        credits.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
