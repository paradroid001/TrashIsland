using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButtons : MonoBehaviour, IPointerClickHandler
{
    public Inventory inventory;
    public InventoryItem item;
    public bool selectable;
    public bool selected;
    public void Start()
    {
        inventory = transform.parent.parent.parent.GetComponent<Inventory>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(selectable == true)
        {
            if(inventory.reason == Inventory.InventoryReason.Bin || inventory.reason == Inventory.InventoryReason.Process)
            {
                Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
                if(selected == false)
                {
                    selected = true;
                    inventory.selected.Add(this);
                }
                else 
                {
                    selected = false;
                    inventory.selected.Remove(this);
                }
            }
            else if(inventory.reason == Inventory.InventoryReason.Look)
            {
                if(!inventory.selected.Contains(this))
                {
                    if(inventory.selected.Count == 0)
                    {
                        inventory.selected.Add(this);
                        inventory.dropButton.SetActive(true);
                        inventory.dropButton.transform.SetParent(transform);
                        inventory.dropButton.transform.localPosition = new Vector2(0, 100);
                        inventory.dropButton.transform.SetParent(inventory.dropButton.GetComponent<DropButton>().firstParent);
                    }
                }
                else
                {
                    inventory.selected.Remove(this);
                    inventory.dropButton.SetActive(false);
                }

            }
        }
        
    }
}
