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
    }
}
