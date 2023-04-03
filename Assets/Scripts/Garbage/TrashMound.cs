using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMound : MonoBehaviour
{
    public List<InventoryItem> produces;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(GameManager.instance.player.equippedTool.name == "Pickaxe")
        {
            int whichTrash = Random.Range(0, produces.Count - 0);
            GameManager.instance.invent.AddToInventory(produces[whichTrash]);
            Debug.Log(produces[whichTrash].name);
        }
    }
}
