using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RobotCompanion : Companion
{
    public GameObject inventoryA;
    public RoboInventory inventoryB;
    public GameObject inventoryC;
    public List<InventoryItem> robotInventory;
    GameObject button;
    public void Start()
    {
        inventoryB = inventoryA.GetComponent<RoboInventory>();
        inventoryC = inventoryA.transform.GetChild(0).GetChild(8).gameObject;
    }
    public void Interact()
    {
        inventoryC.SetActive(true);
        inventoryB.reason = RoboInventory.RoboInventoryReason.Look;
        Time.timeScale = 0;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.parent.parent.parent.GetComponent<Player>();
            player.interactable = true;
            player.Interactables.Add(gameObject);
            button = GameObject.Instantiate<GameObject>(GameManager.instance.player.interactButtonTemplate);
            button.transform.SetParent(GameManager.instance.player.playerCanvas.transform.GetChild(0));
            button.transform.localPosition = Vector2.zero;
            TextMeshProUGUI buttontext = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttontext.text = "S.P.A.R.C.";
            InteractButtons interactButton = button.GetComponent<InteractButtons>();
            interactButton.correspond = gameObject;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.parent.parent.parent.GetComponent<Player>();
            player.interactable = false;
            player.Interactables.Remove(gameObject);
            GameObject.Destroy(button);
        }
    }
}