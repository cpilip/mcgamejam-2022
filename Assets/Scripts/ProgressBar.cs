using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public bool radial;
    public Slider slider;
    private int progVal;
    private TextMeshProUGUI txt;
    private Image img;
    
    int GetProgress(string color)
    {
        // placeholder script
        // will take game object name (color of world) and update based on relevant vars
        if (string.Equals(color, "blue"))
        {
            return 25;
        }
        else if (string.Equals(color, "red"))
        {
            return 66;
        }
        else if (string.Equals(color, "green"))
        {
            return 75;
        }
        return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        txt = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        img = this.gameObject.transform.GetChild(0).GetComponent<Image>();
        progVal = GetProgress(gameObject.name);

        txt.text = $"{progVal}%";
        slider.value = progVal;
        if (radial)
        {
            img.fillMethod = Image.FillMethod.Radial360; // if radial is checked, change fill method to radial.
        }
        else
        {
            img.fillMethod = Image.FillMethod.Vertical; // if radial is not checked, confirm fill method is vertical
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
