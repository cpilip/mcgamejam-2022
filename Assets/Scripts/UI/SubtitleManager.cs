using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    public GameObject subtitleObj;
    private GameObject bo, lo;
    private string line, voiceName, fileName;
    private bool isPlaying;
    private Queue<string> lines;
    private string[] linesArr;
    private int lineCount;
    private int isBeepSpeak;

    private SoundManager smgr;

    // Start is called before the first frame update
    void OnEnable()
    {
        smgr = GameObject.FindWithTag("SFX").GetComponent<SoundManager>();
        bo = GameObject.FindWithTag("Player");
        lo = gameObject;
        isPlaying = false;
        isBeepSpeak = 0;
        lines = new Queue<string>();
        fileName = GetFileName();
        // text copied from file to array
        linesArr = Resources.Load<TextAsset>(fileName).text.Split("\n"[0]);
        lineCount = 0;

        lines.Clear();

        foreach (string line in linesArr)
        {
            lines.Enqueue(line);
            lineCount++;
        }
    }

    private void OnTriggerEnter2D()
    {
        if (!isPlaying)
        {
            bo.GetComponent<CharacterMovementController>().enabled = false;
            StartCoroutine("TypeLine");
            bo.GetComponent<CharacterMovementController>().enabled = true;
        }
    }

    string GetFileName()
    {
        // gets appropriate dialogue based on quest stages
        return "intro";
    }

    IEnumerator TypeLine()
    {
        Debug.Log("Starting coroutine...");
        isPlaying = true;

        while (lineCount > 2)
        {
            line = lines.Dequeue();
            voiceName = line.Substring(0, 2).ToLower();
            line = line.Substring(4);
            var parentTranform = lo.transform;
            if (voiceName.Equals("bo")) // defaults to lo.transform, if name is bo change to bo
            {
                parentTranform = bo.transform;
            }
            var subtitle = Instantiate(subtitleObj, parentTranform);    // instantiates canvas + text child (index 0)
            subtitle.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            foreach (char letter in line.ToCharArray())
            {
                subtitle.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += letter;
                // used to play beep speech only on every other letter
                // makes dialogue faster without being too loud
                if (isBeepSpeak == 3)
                {
                    smgr.BeepSpeak(voiceName);
                    isBeepSpeak = 0;
                }
                isBeepSpeak++;

                yield return new WaitForSeconds(0.03f);
            }
            yield return new WaitForSeconds(0.8f);
            Destroy(subtitle);
        }
        isPlaying = false;
    }
}
