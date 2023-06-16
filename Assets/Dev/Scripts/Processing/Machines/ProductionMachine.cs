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
    public Cleaner cleaner;
    public Compressor compressor;
    public Furnace furnace;
    public Shredder shredder;
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
            Player player = other.transform.GetComponent<Player>();
            player.interactable = true;
            player.Interactables.Add(gameObject);
            button = GameObject.Instantiate<GameObject>(GameManager.instance.player.interactButtonTemplate);
            button.transform.SetParent(GameManager.instance.player.playerCanvas.transform.GetChild(0));
            button.transform.localPosition = Vector2.zero;
            TextMeshProUGUI buttontext = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            if(cleaner != null)
            {
                buttontext.text = "Washer";
            }
            else if(compressor != null)
            {
                buttontext.text = "Crusher";
            }
            else if(furnace != null)
            {
                buttontext.text = "Smelter";
            }
            else if(shredder != null)
            {
                buttontext.text = "Shredder";
            }
            InteractButtons interactButton = button.GetComponent<InteractButtons>();
            interactButton.correspond = gameObject;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
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
        if(compressor != null && compressor.repaired == true || cleaner != null && cleaner.repaired == true || furnace != null && furnace.repaired == true || shredder != null && shredder.repaired == true)
        {
            interacted = true;
            yield return new WaitForSeconds(3);
            Debug.Log("Compressed");
            for(int i = 0; i < contains.Count; i++)
            {
                if(cleaner != null)
                {
                    cleaner.DoThing(contains[i]);
                }
                else if(shredder != null)
                {
                    shredder.DoThing(contains[i]);
                }
                else if(furnace != null)
                {
                    furnace.DoThing(contains[i]);
                }
                else if(compressor != null)
                {
                    compressor.DoThing(contains[i]);
                }
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
}
