using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Recycle : Bin
{
    void Awake()
    {
        binType = BinType.Recycle;
        //GameManager.instance.recycles.Add(this);
    }
    void FixedUpdate()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.interactable = true;
            player.Interactables.Add(gameObject);
            button = GameObject.Instantiate<GameObject>(GameManager.instance.player.interactButtonTemplate);
            button.transform.SetParent(GameManager.instance.player.playerCanvas.transform.GetChild(0));
            button.transform.localPosition = Vector2.zero;
            TextMeshProUGUI buttontext = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttontext.text = "Recycle Bin";
            InteractButtons interactButton = button.GetComponent<InteractButtons>();
            interactButton.correspond = gameObject;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.interactable = false;
            player.Interactables.Remove(gameObject);
            GameObject.Destroy(button);
        }
    }
    public void Interact()
    {
        GameManager.instance.invent.reason = Inventory.InventoryReason.Bin;
        if(GameManager.instance.invent.inventoryItems.Capacity <= 12)
        {
            Debug.Log("Interact");
            GameManager.instance.inventory.SetActive(true);
            GameManager.instance.invent.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            GameManager.instance.invent.currentBin = this;
            Time.timeScale = 0;
        }
        /*switch (trash.trashType)
        {
            case Trash.TrashType1.recycle:
                GameManager.instance.recyclePoints += 10;
                break;
            case Trash.TrashType1.garbage:
                GameManager.instance.recyclePoints -= 10;
                break;
        }*/
    }
}
