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

    [SerializeField] private Sprite m_left;
    [SerializeField] private Sprite m_right;
    [SerializeField] private SpriteRenderer m_renderer;

    void Awake()
    {
        m_renderer = GetComponent<SpriteRenderer>();
    }

    public override void InteractWith()
    {
        m_facingRight = !m_facingRight;
        Flip();

        SceneStateManager.statueStates[(int)m_whatStatue] = m_facingRight;
    }

    public void ResetState()
    {
        m_facingRight = SceneStateManager.statueStates[(int)m_whatStatue];
        
        Flip();
    }

    void Flip()
    {
        if (m_facingRight)
        {
            m_renderer.sprite = m_right;
        }
        else
        {
            m_renderer.sprite = m_left;
        }
    }

}
