using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
public class AnimEventPasser : MonoBehaviour
{
    [SerializeField]
    private TempMovement target;
    public void passToParent()
    {
        target.EnableMovement();
    }

    public void DestroyInteractable()
    {
        target.DeleteObject();
    }

    public void SetBool()
    {
       //target.movementOverride = false;
    }
}
}