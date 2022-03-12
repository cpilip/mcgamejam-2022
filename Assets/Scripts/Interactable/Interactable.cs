using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    public virtual void InteractWidth()
    {
        Debug.LogError(this.gameObject.name + " has no interactWith() function overload.");
    }
}
