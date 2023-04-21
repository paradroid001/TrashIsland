using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trash : MonoBehaviour
{
    public enum TrashType1 {recycle, garbage, organic};
    public TrashType1 trashType;
    public TrashType typeOf;
    public BuriedTrash buriedTrash;
    GameObject button;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(buriedTrash != null)
            {
                buriedTrash.near = true;
            }
            Player player = other.GetComponent<Player>();
            player.interactable = true;
            player.Interactables.Add(gameObject);
            button = GameObject.Instantiate<GameObject>(GameManager.instance.player.interactButtonTemplate);
            button.transform.SetParent(GameManager.instance.player.playerCanvas.transform.GetChild(0));
            button.transform.localPosition = Vector2.zero;
            TextMeshProUGUI buttontext = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttontext.text = typeOf.trashName;
            InteractButtons interactButton = button.GetComponent<InteractButtons>();
            interactButton.correspond = gameObject;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if(buriedTrash != null)
            {
                buriedTrash.near = false;
            }
            Player player = other.GetComponent<Player>();
            player.interactable = false;
            player.Interactables.Remove(gameObject);
            GameObject.Destroy(button);
        }
    }
    public void Interact(Player player)
    {
        if(player.holding != true)
        {
            Debug.Log("Interact");
            if(GameManager.instance.invent.inventoryItems.Capacity <= 12)
            {
                if(buriedTrash == null)
                {
                    //GameManager.instance.invent.inventoryItems.Add(typeOf);
                    player.interactable = false;
                    player.Interactables.Remove(gameObject);
                    gameObject.SetActive(false);
                    GameManager.instance.invent.AddToInventory(typeOf);
                }
                else if(buriedTrash != null)
                {
                    if(buriedTrash.buried == true)
                    {
                        if(GameManager.instance.player.equippedTool.name == "Shovel")
                        {
                            //play digging animation
                            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                            buriedTrash.buried = false;
                        }
                    }
                    else 
                    {
                        player.interactable = false;
                        player.Interactables.Remove(gameObject);
                        gameObject.SetActive(false);
                        GameManager.instance.invent.AddToInventory(typeOf);
                    }
                }
                
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
