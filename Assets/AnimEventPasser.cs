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
}
}