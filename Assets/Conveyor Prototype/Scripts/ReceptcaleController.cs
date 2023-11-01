using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategories
{
    plastic,
    cube,
    sphere,
    capsule,
};
public class ReceptcaleController : MonoBehaviour
{
    private Rigidbody rb; //the rb of the object that has just been received
    private GameObject obj; //the object that has just been received
    [SerializeField]
    private List<ItemCategories> sortList; //the list of Objects we accept
    [SerializeField]
    private ConveyorMinigame manager;

    void OnTriggerEnter(Collider other)
    {
        rb = other.GetComponentInParent<Rigidbody>(); //get the rigidbody of the object that just entered
        obj = rb.gameObject; //gets the root obejct

        if(sortList.Contains(obj.GetComponent<ItemController>().myCategory)) //if the incoming object is from a category we want
        {
            Debug.Log("Good");
            //animate happy effects
            //tick up reward systems
            //call the manager to run a check to see if there are no more objects and therefore end the minigame

        }
        else
        {
            Debug.Log("Bad");
            //animate unhappy effects
            //somehow throw the item back onto the conveyor belt
            StartCoroutine(manager.RejectItem(obj.transform));
            //and set it to be kinematic again
            //put the item back under the 'belt' parent
        }

    }
}
