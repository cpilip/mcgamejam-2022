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
    private bool intro, scene1, puzz1done;
    private Queue<string> lines;
    private string[] linesArr;
    private int lineCount;
    private int isBeepSpeak;
    private SceneStateManager scmgr;

    private SoundManager smgr;

    // Start is called before the first frame update
    void OnEnable()
    {
        smgr = GameObject.FindWithTag("SFX").GetComponent<SoundManager>();
        scmgr = GameObject.FindWithTag("SceneMgr").GetComponent<SceneStateManager>();
        bo = GameObject.FindWithTag("Player");
        lo = gameObject;

        isPlaying = false;
        intro = false;
        scene1 = false;
        puzz1done = false;
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
        }
    }

    string GetFileName()
    {
        // gets appropriate dialogue based on quest stages
        if (!intro)
        {
            return "intro";
        }
        if (intro && !scene1 && puzz1done)
        { }
        return null;
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
            var parentTransform = lo.transform;
            if (voiceName.Equals("bo")) // defaults to lo.transform, if name is bo change to bo
            {
                parentTransform = bo.transform;
            }
            var subtitle = Instantiate(subtitleObj, parentTransform);    // instantiates canvas + text child (index 0)
            subtitle.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            if (bo.transform.localScale.x < 0 && voiceName.Equals("bo"))
            {
                // if player is flipped, flip text box accordingly
                subtitle.transform.localScale = new Vector2(subtitle.transform.localScale.x * -1, subtitle.transform.localScale.y);
            }
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

        bo.GetComponent<CharacterMovementController>().enabled = true;
        isPlaying = false;
    }
}
