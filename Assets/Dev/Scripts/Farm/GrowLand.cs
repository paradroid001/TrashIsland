using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowLand : MonoBehaviour
{
    public Seed grow;
    public int growth;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.parent.parent.parent.GetComponent<Player>();
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
        if(grow == null)
        {
            for(int i = 0; i <= GameManager.instance.invent.inventoryItems.Capacity; i++)
            {
                if(GameManager.instance.invent.inventoryItems[i].seed != null)
                {
                    grow = GameManager.instance.invent.inventoryItems[i].seed;
                    GameManager.instance.invent.inventoryItems.Remove(GameManager.instance.invent.inventoryItems[i]);
                    GameManager.instance.invent.images[i].sprite = null;
                    GameManager.instance.invent.images[i].color = Color.red;
                    StartCoroutine(Grow());
                    break;
                }
            }
            Debug.Log("Plant");
        }
        else if(growth >= grow.maxGrowth)
        {
            GameManager.instance.invent.AddToInventory(grow.food);
        }
        
    }
    public IEnumerator Grow()
    {
        yield return new WaitForSeconds(10);
        growth++;
        
        if(growth <= grow.maxGrowth)
        {
            StartCoroutine(Grow());
            Debug.Log("grow");
        }
    }
}
