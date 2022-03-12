using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private Image timer;
    private bool isRunning;
    private float currentTime;
    public float totalTime;

    void OnTimerEnd()
    {
        // force teleport player back to safe room
        SceneManager.LoadScene("Burrow");
        // switch scene: sceneStateMgr will handle saves
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalTime;
        timer = GetComponent<Image>();
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: flash when low, play sound
        if (currentTime > 0)
        {
            currentTime = currentTime - Time.deltaTime;
            timer.fillAmount = currentTime / totalTime;
        }
        if (currentTime <= 0 && isRunning)
        {
            OnTimerEnd();       // check for is timer running ensure this is called only once
            isRunning = false;
        }
    }
}
