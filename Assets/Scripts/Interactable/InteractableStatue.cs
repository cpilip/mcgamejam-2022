using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum InteractableStatueType
{
    Dog,
    Rabbit,
    Snake,
    Bear
}

public class InteractableStatue : Interactable
{
    [SerializeField] private InteractableStatueType m_whatStatue;
    [SerializeField] private bool m_facingRight; //True = right

    

    public override void InteractWith()
    {
        m_facingRight = !m_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        SceneStateManager.statueStates[(int)m_whatStatue] = m_facingRight;
    }

    public void ResetState()
    {
        m_facingRight = SceneStateManager.statueStates[(int)m_whatStatue];
        Vector3 theScale = transform.localScale;
        if (m_facingRight)
        {
            theScale.x = System.Math.Abs(theScale.x);
            transform.localScale = theScale;
        }
        else
        {
            theScale.x = -1 * System.Math.Abs(theScale.x);
            transform.localScale = theScale;
        }
                
    }

}
