using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMusic : MonoBehaviour
{
    private SoundManager smgr;
    // Start is called before the first frame update
    void Start()
    {
        smgr = GameObject.FindWithTag("SFX").GetComponent<SoundManager>();
        smgr.StartFadeIn("musicGreen");
    }

    private void OnDisable()
    {
        smgr.StartFadeOut("musicGreen");
    }
}
