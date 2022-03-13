using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNote : Interactable
{
    [SerializeField] private bool m_Read = false;
    public override void InteractWith()
    {
        if (!m_Read)
        {
            m_Read = true;
            DisablePing();
            GameObject.FindWithTag("SceneMgr").GetComponent<SceneStateManager>().GetRiddle();
        }
       
    }

    public void ResetState()
    {
       
    }
}
