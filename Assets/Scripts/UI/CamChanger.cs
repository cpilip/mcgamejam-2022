using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    private float previous;

    void OnEnable()
    {
        if (vcam != null)
        {
            previous = vcam.m_Lens.OrthographicSize;
            vcam.m_Lens.OrthographicSize = 5.0f;
        }
    }

    void OnDisable()
    {
        if (vcam != null)
        {
            vcam.m_Lens.OrthographicSize = previous;
        }
    }
}
