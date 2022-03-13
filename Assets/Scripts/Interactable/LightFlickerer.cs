using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerer : MonoBehaviour
{
    [SerializeField] private Transform lights;
    [SerializeField] private Dictionary<int, GameObject> idxToLight = new Dictionary<int, GameObject>();
    [SerializeField] private Dictionary<int, Coroutine> idxToCoroutine = new Dictionary<int, Coroutine>();

    void Awake()
    {
        foreach (Transform l in lights)
        {
            idxToLight.Add(l.GetComponent<InteractableLight>().m_lightIndex, l.gameObject);
            if (!SceneStateManager.lightStates[l.GetComponent<InteractableLight>().m_lightIndex])
            {
                Coroutine c = StartCoroutine(Flicker(l.GetComponent<InteractableLight>().m_lightIndex, l.GetComponent<InteractableLight>().m_flickerWaitTime));
                idxToCoroutine.Add(l.GetComponent<InteractableLight>().m_lightIndex, c);
            }
        }
    }

    public void TurnOffLightFlicker(int i)
    {
        StopCoroutine(idxToCoroutine[i]);
    }

    IEnumerator Flicker(int i, float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            idxToLight[i].SetActive(false);
            
            yield return new WaitForSeconds(time);
            idxToLight[i].SetActive(true);
            
        }
    }
    

}
