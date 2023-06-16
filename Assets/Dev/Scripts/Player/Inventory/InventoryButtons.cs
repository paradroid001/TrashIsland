using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InventoryButtons : InvButtons, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Inventory inventory;
    public InventoryItem item;
    public TextMeshProUGUI number;
    public bool selectable;
    public bool selected;
    public void Start()
    {
        inventory = transform.parent.parent.parent.GetComponent<Inventory>();
        number = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void OnValidate()
    {
        inventoryButtons = this;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(selectable == true)
        {
            if(inventory.reason == Inventory.InventoryReason.Bin && inventory.organise == false|| inventory.reason == Inventory.InventoryReason.Process && inventory.organise == false)
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
            else if(inventory.reason == Inventory.InventoryReason.Look && inventory.organise == false)
            {
                if(!inventory.selected.Contains(this))
                {
                    if(inventory.selected.Count == 0)
                    {
                        inventory.selected.Add(this);
                        inventory.dropButton.SetActive(true);
                        if(inventory.selected.Contains(this))
                        {
                            TextMeshProUGUI nameDisplay = GameManager.instance.invent.nameDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                            nameDisplay.text = item.name;
                        }         
                        else
                        {
                            TextMeshProUGUI nameDisplay = GameManager.instance.invent.nameDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                            nameDisplay.text = "";
                        }           
                        /*inventory.dropButton.transform.SetParent(transform);
                        inventory.dropButton.transform.localPosition = new Vector2(0, 100);
                        inventory.dropButton.transform.SetParent(inventory.dropButton.GetComponent<DropButton>().firstParent);*/
                    }
                }
                else
                {
                    inventory.selected.Remove(this);
                    inventory.dropButton.SetActive(false);
                    TextMeshProUGUI nameDisplay = GameManager.instance.invent.nameDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                    nameDisplay.text = "";
                }

            }
            else if(inventory.reason == Inventory.InventoryReason.Look && inventory.organise == true)
            {
                //Debug.Log("L");
                if(inventory.swap.Count != 0)
                {
                    //swap the things
                    if(inventory.swap.Contains(this))
                    {
                        inventory.swap.Remove(this);
                    }
                    else
                    {
                        inventory.swap.Add(this);
                        inventory.MoveStuff();
                    }
                }
                else if(inventory.swap.Count == 0)
                {
                    if(!inventory.swap.Contains(this))
                    {
                        inventory.swap.Add(this);
                    }
                }
            }
        }
        if(selected == true)
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
        else if(selected == false)
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(selectable && item != null)
        {
            //Debug.Log("A");
            //TextMeshProUGUI nameDisplay = GameManager.instance.invent.nameDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            //nameDisplay.transform.parent.gameObject.SetActive(true);
            //nameDisplay.text = item.name;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(item != null && selectable)
        {
            //TextMeshProUGUI nameDisplay = GameManager.instance.invent.nameDisplay.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            //nameDisplay.transform.parent.gameObject.SetActive(false);
            //nameDisplay.text = "";
        }
    }
}
