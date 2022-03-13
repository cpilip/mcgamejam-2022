using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Warp : MonoBehaviour
{
    public int warpTime;
    private int currentTime;
    private bool isRunning;
    private SpriteRenderer white;
    private SoundManager smgr;

    public void Teleport()
    {
        GetComponent<CharacterMovementController>().enabled = false;
        StartCoroutine("Tp");
    }

    // Start is called before the first frame update
    void Start()
    {
        isRunning = false;
        white = gameObject.transform.GetChild(3).GetComponent<SpriteRenderer>(); // prefab asserts child order, white image will be 3rd
        Debug.Log("Acquired Sprite Renderer of GameObject \"" + gameObject.transform.GetChild(3).name + "\"");
        smgr = GameObject.FindWithTag("SFX").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // get input, while key is held increase alpha of white layer, then teleport
        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            if (Input.GetKeyDown(KeyCode.T) && isRunning == false)
            {
                isRunning = true;       // prevents repeating same coroutine
                currentTime = warpTime; // reset time variable
                GetComponent<CharacterMovementController>().enabled = false;
                StartCoroutine("Tp");
            }
            if (Input.GetKeyUp(KeyCode.T) && isRunning == true)
            {
                isRunning = false;      // reset bool
                GetComponent<CharacterMovementController>().enabled = true;
                white.color = new Color(255, 255, 255, 0);
                Debug.Log("key up");
                smgr.Stop("warp");
                StopCoroutine("Tp");
            }
        }
    }

    IEnumerator Tp ()
    {
        var tempColor = new Color(255, 255, 255, 0);
        smgr.Play("warp");
        while (white.color.a < 1f)
        {
            tempColor = new Color(255, 255, 255, tempColor.a + (Time.deltaTime / (float)warpTime));
            white.color = tempColor;
            yield return null;
        }
        GetComponent<CharacterMovementController>().enabled = true;
        isRunning = false;
        SceneManager.LoadScene("Burrow");
        // white.color = new Color(255, 255, 255, 0);
    }
}
