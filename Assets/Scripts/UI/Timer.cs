using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private GameObject player;

    private Image timer;
    private bool isRunning;
    private bool flash50, flash10;
    private float currentTime;
    public float totalTime;

    void OnTimerEnd()
    {
        // force teleport player back to safe room
        player.GetComponent<CharacterMovementController>().enabled = false;
        player.GetComponent<Warp>().StartCoroutine("Tp");
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalTime;
        timer = GetComponent<Image>();
        isRunning = true;
        flash50 = false;
        flash10 = false;

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: flash when low, play sound
        if (currentTime > 0)
        {
            currentTime = currentTime - Time.deltaTime;
            timer.fillAmount = currentTime / totalTime;
            if (currentTime <= totalTime/2 && flash50 == false)  // flash at halfway point
            {
                Debug.Log("Timer 50%; currentTime = " + currentTime);
                flash50 = true;
                StartCoroutine("Flash");
            }
            if (currentTime <= totalTime/10 && flash10 == false)  // flash again at 10% if not flashing
            {
                Debug.Log("Timer 10%; currentTime = " + currentTime);
                flash10 = true;
                StartCoroutine("Flash");
            }
        }
        if (currentTime <= 0 && isRunning)
        {
            OnTimerEnd();       // check for is timer running ensure this is called only once
            isRunning = false;
        }
    }

    IEnumerator Flash ()
    {
        Debug.Log("Starting Coroutine...");
        var white = new Color(255, 255, 255);
        var yellow = new Color32(255, 244, 59, 255);
        for (int i = 0; i < 4; i++)     // run 3 times
        {
            if (timer.color.b == 255)   // if color is white, turn yellow
            {
                Debug.Log("color is white");
                timer.color = yellow;
            }
            else
            {
                timer.color = white;    // else turn white
            }
            yield return new WaitForSeconds(.1f);
        }
        timer.color = white;
    }
}
