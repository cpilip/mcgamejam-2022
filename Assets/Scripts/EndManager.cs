using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;

public class EndManager : MonoBehaviour
{

    //public Image element;
    void OnEnable()
    {
        StartCoroutine("fadeEnd");


    }

    public IEnumerator fadeEnd()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            this.GetComponent<Image>().color = new Color(0, 0, 0, i);
            yield return null;
        }

        transform.parent.GetChild(2).gameObject.SetActive(true);


    }

    public void ExitGame()
    {
        Application.Quit();
    }

}

