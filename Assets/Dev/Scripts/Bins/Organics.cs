using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Organics : Bin
{
    public float waitTime;
    public Compost compost;
    void Awake()
    {
        binType = BinType.Organic;
    }
    void FixedUpdate()
    {
        if(hasThings == true && sent == false)
        {
            StartCoroutine(SendToCompost());
            sent = true;
        }
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
            buttontext.text = "Organic Bin";
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
    }
    public IEnumerator SendToCompost()
    {
        yield return new WaitForSeconds(waitTime);
        DroneManager.instance.SendToBin(this);
        Debug.Log(waitTime);
        for(int i = 0; i < inBin.Capacity; i++)
        {
            compost.thingsIn.Add(inBin[i]);
        }
        //inBin.Clear();
        hasThings = false;
    }
}
