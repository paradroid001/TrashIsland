using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Compost : MonoBehaviour
{
    public List<InventoryItem> thingsIn;
    public List<Seed> seeds;
    public Seed seed;
    public SpriteRenderer sprite;
    public bool seedPresented;
    void Start()
    {
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(thingsIn.Capacity >= 10 && seedPresented == false)
        {
            int whichSeed = Random.Range(0, 3);
            Debug.Log(whichSeed);
            sprite.sprite = seeds[whichSeed].inventorySprite;
            seed = seeds[whichSeed];
            thingsIn.Clear();
            seedPresented = true;
        }
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
            Player player = other.transform.parent.parent.parent.GetComponent<Player>();
            player.interactable = false;
            player.Interactables.Remove(gameObject);
        }
    }
    public void Interact()
    {
        if(seedPresented == true)
        {
            Debug.Log("Give Seed");
            GameManager.instance.invent.AddToInventory(seed);
        }
        
    }
}
