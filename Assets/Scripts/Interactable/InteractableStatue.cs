using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum InteractableStatueType
{
    Dog,
    Rabbit,
    Human,
    Snake
}

public class InteractableStatue : MonoBehaviour, Interactable
{
    [SerializeField] private InteractableStatueType m_whatStatue;
    [SerializeField] private bool m_facingRight;

    void InteractWith()
    {
        m_facingRight = !m_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        
    }


}
