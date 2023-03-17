using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCompanion : Companion
{
    public GameObject inventoryA;
    public inventory inventoryB;
    public GameObject inventoryC;
    public List<InventoryItem> robotInventory;
    public void Start()
    {
        inventoryB = inventoryA.transform.parent.GetComponent<inventory>();
        inventoryC = inventoryA.transform.parent.GetChild(2).gameObject;
    }
    public void Interact()
    {
        inventoryC.SetActive(true);
        //inventoryB.reason = inventory.InventoryReason.Look;
        Time.timeScale = 0;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.interactable = true;
            player.Interactables.Add(gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.interactable = false;
            player.Interactables.Remove(gameObject);
        }
    }
}