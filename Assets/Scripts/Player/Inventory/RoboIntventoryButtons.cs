using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoboIntventoryButtons : MonoBehaviour
{
    public InventoryItem item;
    public RoboInventory inventory;
    public bool selectable;
    void Start()
    {
        inventory = transform.parent.parent.parent.parent.GetComponent<RoboInventory>();
    }
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(selectable == true)
        {
            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        }
    }
}
