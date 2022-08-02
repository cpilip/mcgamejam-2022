using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableNote : Interactable
{
    [SerializeField] private bool m_Read = false;

    public GameObject riddleMenu;
    private GameObject menu;
    private bool gotRiddle, menuUp;
    private string sceneName;

    public override void InteractWith()
    {
        if (!m_Read)
        {
            Debug.Log("reading note");
            m_Read = true;
            DisablePing();
            GetRiddle();
        }
       
    }

    public void ResetState()
    {
       
    }

    public void GetRiddle()
    {
        gotRiddle = true;
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        ShowRiddle();
    }

    void ShowRiddle()
    {
        menuUp = true;
        menu = Instantiate(riddleMenu, FindObjectOfType<Canvas>().transform);
        switch (sceneName)
        {
            case "Red":
                // riddleBox prefab is sorted BKGD->PANEL->TEXT
                // GetChild statements navigate to the text box of the prefab
                menu.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "You can't be caught, do not be killed\n"
                                                                                            + "Your blood must pump, must not be spilled.\n"
                                                                                            + "So keep your eyes upon the beast\n"
                                                                                            + "Who sees you there, an evening feast\n"
                                                                                            + "And pray the one, with kit in tow,\n"
                                                                                            + "She will not see the fangs below\n"
                                                                                            + "The blades of grass and gleaming eyes\n"
                                                                                            + "That stare and spell his sure demise.";
                break;

            case "Green":
                menu.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "The wind shall blow, the rain shall fall\n"
                                                                                            + "And to traverse this ancient hall\n"
                                                                                            + "The song of seasons you must sing,\n"
                                                                                            + "To hide among the verdant spring.\n"
                                                                                            + "To choose the one that does not match\n"
                                                                                            + "Shall send you tumbling down the hatch\n";
                break;

            case "Blue":
                menu.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Dark cannot be seen, cannot be felt,\n"
                                                                                            + "Cannot be heard, cannot be smelt.\n"
                                                                                            + "The more you have, the less you'll see.\n"
                                                                                            + "Here in the deep and dim black sea.\n"
                                                                                            + "We welcome night by twinkling bright,\n"
                                                                                            + "so catch us all to bring the light.";
                break;
            default:
                Debug.Log("No riddle found in scene");
                break;
        }
        // play page turn pickup sound effect
        smgr.Play("page");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!menuUp)
            {
                if (gotRiddle)
                {
                    ShowRiddle();
                }
            }
            else if (menuUp)
            {
                Destroy(menu);
                menuUp = false;
            }
        }
        // E opens menu when interacted with, should also close it
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            if (menuUp && gotRiddle)
            {
                Destroy(menu);
            }
        }*/
    }
}
