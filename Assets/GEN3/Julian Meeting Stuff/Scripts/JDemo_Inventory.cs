using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class JDemo_Inventory : MonoBehaviour
{
    
    public string heldItemName;
    
    public int heldItemID;
    public int heldItemProgression;
    
    public bool playerHasItem;

    [YarnCommand ("GiveItem")]
    public void AttemptAddItem(string itemName, int itemID, int itemProg)
    {
        if (!playerHasItem)
        {
            heldItemName = itemName;
            heldItemID = itemID;
            playerHasItem = true;
        }
        else
        {
            Debug.Log("Error adding item to inventory of " +gameObject.name+", as there is already an item stored. inventory must be cleared first (use ClearInventory() function)");
        }
    }

    [YarnCommand ("ClearInventory")]
    public void ClearInventory()
    {
        heldItemName = null;
        heldItemID = 0;
        heldItemProgression = 0;
        playerHasItem = false;
    }
}
