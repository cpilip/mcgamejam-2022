using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMusic : MonoBehaviour
{
    private SoundManager smgr;
    // Start is called before the first frame update
    void Start()
    {
        smgr = GameObject.FindWithTag("SFX").GetComponent<SoundManager>();
        smgr.StartFadeIn("musicRed");
    }

    private void OnDisable()
    {
        smgr.StartFadeOut("musicRed");
    }
}
