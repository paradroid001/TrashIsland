using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButtons : MonoBehaviour
{
    public GameObject correspond;
    public Inventory follower;
    public void Start()
    {
        follower = GameManager.instance.invent;
    }
    public void Interact()
    {
        Debug.Log(correspond);
        Canvas grndParent = transform.parent.parent.GetComponent<Canvas>();
        if(correspond.GetComponent<Trash>() == false && correspond.GetComponent<TrashMound>() == false && correspond.GetComponent<PickupEvent>() == false && correspond.GetComponent<ProductionMachine>() == false)
        {
            follower.GetComponent<Canvas>().sortingOrder = 2;
        }
        GameManager.instance.player.interacting = correspond;
        GameManager.instance.player.Interact();
        if(correspond.GetComponent<Trash>() == true || correspond.GetComponent<PickupEvent>() == true)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
