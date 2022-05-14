using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractor : MonoBehaviour
{
    [SerializeField] private GameObject m_current;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        m_current = null;
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && m_current)
        {
            //Debug.Log("Interacted with " + m_current.gameObject.name);
            m_current.SendMessage("InteractWith");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            m_current = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            if (other.gameObject.Equals(m_current))
            {
                m_current = null;
            }
        }
    }
}
