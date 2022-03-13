using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum InteractableCharmType
{
    RedCharm,
    BlueCharm,
    GreenCharm,
}
public class InteractableCharm : Interactable
{
    [SerializeField] private InteractableCharmType m_whatCharm;

    private void Awake()
    {
        m_ping = transform.GetChild(0).gameObject;
    }

    public override void InteractWith()
    {
        if (m_whatCharm == InteractableCharmType.RedCharm && SceneStateManager.statueStates[0] && !SceneStateManager.statueStates[1] && SceneStateManager.statueStates[2] && !SceneStateManager.statueStates[3])
        {
            Debug.Log("Solved Red Puzzle.");
            SceneStateManager.m_puzzleSolvedRed = true;
        }
        else if (m_whatCharm == InteractableCharmType.BlueCharm)
        {
            Debug.Log(m_whatCharm + " puzzle not implemented.");
        }
        else if (m_whatCharm == InteractableCharmType.GreenCharm && SceneStateManager.wallStates[0] && SceneStateManager.wallStates[1] && SceneStateManager.wallStates[2] && SceneStateManager.wallStates[3])
        {
            Debug.Log("Solved Green Puzzle.");
            SceneStateManager.m_puzzleSolvedGreen = true;
        }
        else
        {
            Debug.Log(m_whatCharm + " puzzle not solved.");
        }
    }

}
