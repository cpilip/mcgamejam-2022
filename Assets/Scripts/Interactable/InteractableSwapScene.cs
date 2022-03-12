using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableSwapScene : MonoBehaviour, Interactable
{
    public void InteractWith()
    {
        SceneManager.LoadScene("Green");
    }
}
