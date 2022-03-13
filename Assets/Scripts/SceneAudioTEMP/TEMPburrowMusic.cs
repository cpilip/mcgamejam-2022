using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPburrowMusic : MonoBehaviour
{
    private SoundManager smgr;
    private SpriteRenderer sil;
    // Start is called before the first frame update
    void Start()
    {
        smgr = GameObject.FindWithTag("SFX").GetComponent<SoundManager>();
        smgr.StartFadeIn("musicBurrow");

        // resets player's white silhouette
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(3).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }


    private void OnDisable()
    {
        smgr.StartFadeOut("musicBurrow");
    }
}
