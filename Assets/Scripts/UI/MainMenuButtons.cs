using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject credits;

    private void Awake()
    {
        credits.SetActive(false);
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