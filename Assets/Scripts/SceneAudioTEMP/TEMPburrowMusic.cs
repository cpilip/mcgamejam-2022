using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPburrowMusic : MonoBehaviour
{
    private SoundManager smgr;
    // Start is called before the first frame update
    void Start()
    {
        smgr = GameObject.FindWithTag("SFX").GetComponent<SoundManager>();
        smgr.StartFadeIn("musicBurrow");
    }

    private void OnDisable()
    {
        smgr.StartFadeOut("musicBurrow");
    }
}
