using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum InteractableSeasonType
{
    Spring,
    Summer,
    Fall,
    Winter
}

public class InteractableSeason : Interactable
{
    [SerializeField] private InteractableSeasonType m_whatSeason;
    [SerializeField] private GameObject m_wall;
    [SerializeField] private GameObject m_platform;
    private Renderer m_pRenderer;
    private Renderer m_sRenderer;

    void Awake()
    {
        m_pRenderer = m_platform.GetComponent<Renderer>();
        m_sRenderer = GetComponent<Renderer>();
    }

    public override void InteractWith()
    {
        switch (m_whatSeason)
        { 
            case InteractableSeasonType.Spring:
                Debug.Log(m_whatSeason + " was successful.");
                SceneStateManager.wallStates[(int)m_whatSeason] = true;
                m_wall.SetActive(false);
                GetComponent<Collider2D>().enabled = false;
                m_sRenderer.enabled = false;
                DisablePing();
                break;
            case InteractableSeasonType.Summer:
                if (SceneStateManager.wallStates[0])
                {
                    Debug.Log(m_whatSeason + " was successful.");
                    SceneStateManager.wallStates[(int)m_whatSeason] = true;
                    m_wall.SetActive(false);
                    GetComponent<Collider2D>().enabled = false;
                    m_sRenderer.enabled = false;
                    DisablePing();
                }
                else
                {
                    StartCoroutine("FadeOutPlatform");
                }
                break;
            case InteractableSeasonType.Fall:
                if (SceneStateManager.wallStates[1])
                {
                    Debug.Log(m_whatSeason + " was successful.");
                    SceneStateManager.wallStates[(int)m_whatSeason] = true;
                    m_wall.SetActive(false);
                    GetComponent<Collider2D>().enabled = false;
                    m_sRenderer.enabled = false;
                    DisablePing();
                }
                else
                {
                    StartCoroutine("FadeOutPlatform");
                }
                break;
            case InteractableSeasonType.Winter:
                if (SceneStateManager.wallStates[2])
                {
                    Debug.Log(m_whatSeason + " was successful.");
                    SceneStateManager.wallStates[(int)m_whatSeason] = true;
                    m_wall.SetActive(false);
                    GetComponent<Collider2D>().enabled = false;
                    m_sRenderer.enabled = false;
                    DisablePing();
                }
                else
                {
                    StartCoroutine("FadeOutPlatform");
                }
                break;

            
        }
    }

    public void ResetState()
    {
        bool wasDisabled = SceneStateManager.wallStates[(int)m_whatSeason];
        m_wall.SetActive(!wasDisabled);
        GetComponent<Collider2D>().enabled = !wasDisabled;
        m_sRenderer.enabled = !wasDisabled;
        if (wasDisabled) 
        {
            DisablePing(); 
        }
    }
    IEnumerator FadeOutPlatform()
    {
        while (m_pRenderer.material.color.a > 0)
        {
            Color c = m_platform.GetComponent<Renderer>().material.color;
            float fadeAmt = c.a - (2f * Time.deltaTime);

            c = new Color(c.r, c.g, c.b, fadeAmt);
            m_pRenderer.material.color = c;
            yield return null;
        }
        m_platform.SetActive(false);
        yield return new WaitForSeconds(2f);
        StartCoroutine("FadeInPlatform");
    }
    IEnumerator FadeInPlatform()
    {
        m_platform.SetActive(true);
        while (m_pRenderer.material.color.a < 1)
        {
            Color c = m_platform.GetComponent<Renderer>().material.color;
            float fadeAmt = c.a + (5f * Time.deltaTime);

            c = new Color(c.r, c.g, c.b, fadeAmt);
            m_pRenderer.material.color = c;
            yield return null;

        }
    }
}
