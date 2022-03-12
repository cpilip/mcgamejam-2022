using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private Image timer;
    private bool isRunning;
    private float currentTime;
    public float totalTime;

    void OnTimerEnd()
    {
        // force teleport player back to safe room
        Debug.Log("timer end");
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
