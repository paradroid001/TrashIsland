using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class RoboIntventoryButtons : InvButtons, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItem item;
    public RoboInventory inventory;
    public bool used;
    public bool selectable;
    public TextMeshProUGUI number;
    void Start()
    {
        inventory = GameManager.instance.invent.transform.GetComponent<RoboInventory>();
        
    }
    public void OnValidate()
    {
        roboIntventoryButtons = this;
    }
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(selectable == true && inventory.organise == true)
        {
            if(GameManager.instance.invent.swap.Count != 0)
            {
                if (GameManager.instance.invent.swap.Contains(this))
                {
                    GameManager.instance.invent.swap.Remove(this);
                }
                else if(!GameManager.instance.invent.swap.Contains(this))
                {
                    //swapPos
                    GameManager.instance.invent.swap.Add(this);
                    GameManager.instance.invent.MoveStuff();
                }
            }
            else if(GameManager.instance.invent.swap.Count == 0)
            {
                if(!GameManager.instance.invent.swap.Contains(this))
                {
                    GameManager.instance.invent.swap.Add(this);
                }
            }
            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(selectable && item != null)
        {
            //Debug.Log("A");
            TextMeshProUGUI nameDisplay = GameManager.instance.invent.nameDisplay.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            nameDisplay.transform.parent.gameObject.SetActive(true);
            nameDisplay.text = item.name;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(item != null && selectable)
        {
            TextMeshProUGUI nameDisplay = GameManager.instance.invent.nameDisplay.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            nameDisplay.transform.parent.gameObject.SetActive(false);
        }
    }
}
