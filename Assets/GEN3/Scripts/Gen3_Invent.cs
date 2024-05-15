using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen3_Invent : MonoBehaviour
{
    public List<Gen3_InventItem> items;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void AddToInventory(PickupItem pickedUp)
    {
        if(items.Count != 0)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].item.name == pickedUp.item.name)
                {
                    items[i].amount++;
                    Debug.Log("Added more to this item");
                }
                else 
                {
                    items.Add(new Gen3_InventItem(pickedUp.item, 1));
                    Debug.Log("Added first instance of item to inventory");
                }
            }
        }
        else 
        {
            items.Add(new Gen3_InventItem(pickedUp.item, 1));
            Debug.Log("Added first item to inventory");
        }
    }
    public bool CheckForItem(InventoryItem item)
    {
        for(int i = 0; i <= items.Count; i++)
        {
            if(items[i].item == item && i < items.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public void RemoveFromInventory(int itemRef, string reason)
    {
        switch(reason)
        {
            default:

            break;
            case "Give":
                if(items.Count != 0)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].item.name == items[itemRef].item.name)
                {
                    items[i].amount--;
                    Debug.Log("Added more to this item");
                    if(items[i].amount <= 0)
                    {
                        items.Remove(items[i]);
                    }
                }
            }
        }
            break;
            case "Machine":

            break;
            case "Craft":

            break;
        }
    }
}
[System.Serializable]
public class Gen3_InventItem
{
    public InventoryItem item;
    public int amount;
    public Gen3_InventItem(InventoryItem Item, int Amount)
    {
        item = Item;
        amount = Amount;
    }
}