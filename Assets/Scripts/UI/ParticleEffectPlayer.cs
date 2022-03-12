using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectPlayer : MonoBehaviour
{
    private ParticleSystem m_pSys;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(5);

    void Awake()
    {
        m_pSys = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        StartCoroutine("RepeatEffect");
    }


    IEnumerator RepeatEffect()
    {
        while (true)
        {
            m_pSys.Play();
            yield return waitForSeconds;
        }
    }
}
