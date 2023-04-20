using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProductionMachine : MonoBehaviour
{
    [System.Flags]
    public enum Takes
    {
        none = 0,
        plastic = 1,
        aluminium = 2,
        wood = 4, 
        wires = 8,
        glass = 16, 
        cardboard = 32,
        paper = 64,
        steel = 128
    }
    public int hp;
    public bool broken;
    public Takes takes;
    public List<TrashType> contains;
    public int capacity;
    public MaterialItem produces;
    public bool interacting;
    public bool interacted;
    GameObject button;
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
            Player player = other.GetComponent<Player>();
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
            Player player = other.GetComponent<Player>();
            player.interactable = false;
            player.Interactables.Remove(gameObject);
            GameObject.Destroy(button);
        }
    }
    public void PutIn(InventoryItem item)
    {
        contains.Add(item.trashType);
    }
    public IEnumerator Interact()
    {
        interacted = true;
        yield return new WaitForSeconds(3);
        Debug.Log("Compressed");
        for(int i = 0; i < contains.Count; i++)
        {
            GameManager.instance.materials.Add(produces);
            Debug.Log("0");
            hp -= contains[i].hpCost;
            if(hp == 0)
            {
                broken = true;
            }
        }
        contains.Clear();
        
        interacting = false;
        interacted = false;
    }
}
