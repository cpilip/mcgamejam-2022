using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLight : Interactable
{
    private Renderer m_pRenderer;
    [SerializeField] private LightFlickerer lights;
    public int m_lightIndex;
    public float m_flickerWaitTime;
    

    void Awake()
    {
        m_pRenderer = GetComponent<Renderer>();
    }
    public override void InteractWith()
    {
        Debug.Log(gameObject.name + " was successful.");
        lights.TurnOffLightFlicker(m_lightIndex);
        SceneStateManager.lightStates[m_lightIndex] = true;
        GetComponent<Collider2D>().enabled = false;
        DisablePing();
        StartCoroutine("FadeOutLight");
    }

    public void ResetState()
    {
        bool wasCaught = SceneStateManager.lightStates[m_lightIndex];
        GetComponent<Collider2D>().enabled = !wasCaught;
        if (wasCaught)
        {
            m_pRenderer.material.color = new Color(m_pRenderer.material.color.r, m_pRenderer.material.color.g, m_pRenderer.material.color.b, 0);
            DisablePing();
        }
    }
    IEnumerator FadeOutLight()
    {
        while (m_pRenderer.material.color.a > 0)
        {
            Color c = m_pRenderer.material.color;
            float fadeAmt = c.a - (2f * Time.deltaTime);

            c = new Color(c.r, c.g, c.b, fadeAmt);
            m_pRenderer.material.color = c;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
    }

    

}

