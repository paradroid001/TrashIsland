using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionMachine : MonoBehaviour
{
    [System.Flags]
    public enum Takes
    {
        none = 0,
        plastic = 1,
        metal = 2,
        wood = 4, 
        wires = 8
    }
    public Takes takes;
    public List<TrashType> contains;
    public int capacity;
    public MaterialItem produces;
    public bool interacting;
    public bool interacted;
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
    public void PutIn(InventoryItem item)
    {
        contains.Add(item.trashType);
    }
    public IEnumerator Interact()
    {
        interacted = true;
        yield return new WaitForSeconds(3);
        Debug.Log("Compressed");
        for(int i = 0; i < contains.Capacity; i++)
        {
            GameManager.instance.materials.Add(produces);
            Debug.Log("0");
        }
        contains.Clear();
        interacting = false;
        interacted = false;
    }
}
