using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButtons : MonoBehaviour, IPointerClickHandler
{
    public inventory Inventory;
    public InventoryItem item;
    public bool selectable;
    public bool selected;
    public void Start()
    {
        Inventory = transform.parent.parent.parent.GetComponent<inventory>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(selectable == true)
        {
            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
            if(selected == false)
            {
                selected = true;
                Inventory.selected.Add(this);
            }
            else 
            {
                selected = false;
                Inventory.selected.Remove(this);
            }
        }
    }
}
