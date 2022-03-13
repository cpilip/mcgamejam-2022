using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    public string fileName;
    public string voiceName;
    private string line;
    private TextMeshProUGUI subtitle;
    private bool isPlaying;

    private SoundManager smgr;

    // Start is called before the first frame update
    void Start()
    {
        line = Resources.Load<TextAsset>(fileName).text;
        smgr = GameObject.FindWithTag("SFX").GetComponent<SoundManager>();

        // canvas -> tmp should be the only children of LO
        subtitle = gameObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        subtitle.text = "";
        isPlaying = false;
    }

    void OnTriggerEnter2D()
    {
        if (!isPlaying)
            StartCoroutine("TypeLine");
    }

    IEnumerator TypeLine()
    {
        isPlaying = true;
        subtitle.text = "";
        foreach (char letter in line.ToCharArray())
        {
            subtitle.text += letter;
            smgr.BeepSpeak(voiceName);
            yield return new WaitForSeconds(.1f);
        }
        isPlaying = false;
    }
}
