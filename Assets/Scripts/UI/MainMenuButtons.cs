using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject credits, settings;
    private SoundManager smgr;

    private void Start()
    {
        credits.SetActive(false);
        settings.SetActive(false);
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
    public void OpenSettings()
    {
        settings.SetActive(true);
    }
    public void CloseSettings()
    {
        settings.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
