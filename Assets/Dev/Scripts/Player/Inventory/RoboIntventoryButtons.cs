using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class RoboIntventoryButtons : MonoBehaviour
{
    public InventoryItem item;
    public RoboInventory inventory;
    public bool used;
    public bool selectable;
    public TextMeshProUGUI number;
    void Start()
    {
        inventory = transform.parent.parent.parent.parent.GetComponent<RoboInventory>();
        
    }
    /*public void OnValidate()
    {
        number = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }*/
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
