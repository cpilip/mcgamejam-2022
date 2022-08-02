using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected GameObject m_ping = null;
    protected SoundManager smgr;
    private void Awake()
    {
        m_ping = transform.GetChild(0).gameObject;
        smgr = GameObject.FindWithTag("SFX").GetComponent<SoundManager>();
    }
    public virtual void InteractWith()
    {
    }

    public void DisablePing()
    {
        if (m_ping)
        {
            m_ping.SetActive(false);
        }
    }

    public void EnablePing()
    {
        if (m_ping)
        {
            m_ping.SetActive(true);
        }
    }
}
