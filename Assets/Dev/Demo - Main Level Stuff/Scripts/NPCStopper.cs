using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TrashIsland
{
public class NPCStopper : MonoBehaviour
{
    private bool isActive;
    private Demo_InteractableNPC npc;

    public void AwaitForNPC(Demo_InteractableNPC o)
    {
        isActive = true;
        npc = o;
    }

    private void OnTriggerEnter(Collider col) 
    {
        if (isActive && col.GetComponent<Demo_InteractableNPC>() == npc)
        {
            npc.EndMotion();
            
            isActive = false;
            npc = null;
        }
    }
}
}