using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public enum TrashType1 {recycle, garbage, organic};
    public TrashType1 trashType;
    public TrashType typeOf;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.interactable = true;
            player.Interactables.Add(gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.interactable = false;
            player.Interactables.Remove(gameObject);
        }
    }
    public void Interact(Player player)
    {
        if(player.holding != true)
        {
            Debug.Log("Interact");
            if(player.inventory.Capacity <= 12)
            {
                player.inventory.Add(typeOf);
                player.interactable = false;
                player.Interactables.Remove(gameObject);
                gameObject.SetActive(false);
                GameManager.instance.invent.AddToInventory(typeOf);
                
            }
            //transform.parent = player.hold;
            //player.holding = true;
            //player.interactable = false;
            //player.Interactables.Remove(gameObject);
            //transform.localPosition = Vector3.zero;
        }
    }
    public void NPCInteract(Collector npc)
    {
        //npc pick up trash
        if(npc.holding != true)
        {
            Debug.Log("Interact");
            transform.parent = npc.hold;
            npc.holding = true;
            npc.whichTrash = null;
            npc.collected = true;
            npc.holding = false;
            transform.localPosition = Vector3.zero;
        }
    }
}
