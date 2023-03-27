using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log(waitTime);
        for(int i = 0; i < inBin.Capacity; i++)
        {
            compost.thingsIn.Add(inBin[i]);
        }
        inBin.Clear();
        hasThings = false;
    }
}
