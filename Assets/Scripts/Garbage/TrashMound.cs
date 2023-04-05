using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrashMound : MonoBehaviour
{
    public int hp = 200;
    public List<InventoryItem> produces;
    public Sprite sprite;
    public Image img;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 50 && hp >= 25)
        {
            //change model
        }
        else if(hp < 25 && hp > 0)
        {
            //change model
        }
        else if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            img.sprite = sprite;
            img.color = Color.white;
            Player player = other.GetComponent<Player>();
            player.interactable = true;
            player.Interactables.Add(gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            img.sprite = null;
            img.color = Color.clear;
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
            hp--;
        }
    }
}
