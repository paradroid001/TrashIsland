using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace TrashIsland
{
public enum ItemCategories
{
    waste,
    organic,
    recyclable
};
public class ReceptacleController : MonoBehaviour
{
    //Receiving items
    private Rigidbody rb; //the rb of the object that has just been received
    private GameObject obj; //the object that has just been received
    [SerializeField]
    private List<ItemCategories> sortList; //the list of Objects we accept
    [SerializeField]
    private ConveyorMinigame manager;
    public Transform holdingParent;
    public Transform storedParent;

    private List<Transform> containedItems; //the items currently within the collider
    public float guideStrength; //the strength of the guiding pulling force
    //effects and animators
    [SerializeField]
    private GameObject particles;

    [Header("Bin Animation Controllers")]
    [Space(5)]
    [SerializeField]
    private Animator binAn;
    [SerializeField]
    private Animator faceAn;

    void Start()
    {
        containedItems = new List<Transform>(); //set up the list
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponentInParent<Rigidbody>();
        Transform item = rb.transform;
        if(item.parent.GetComponent<ReceptacleController>()) return; //if the item is already a child of a receptacle
        item.parent = holdingParent; //we are holding on to this object

        containedItems.Add(item); //add the item to the list
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponentInParent<Rigidbody>();
        Transform item = rb.transform;
        //other.isTrigger = true;
        rb.isKinematic = true;

        containedItems.Remove(item); //remvoe the item from the list
    }

    void FixedUpdate()
    {
        GuideItems(); //move contained items towards recepticle transform
    }

    void GuideItems()
    {
        foreach(Transform item in containedItems.ToList())
        {
            Vector3 guideDirection = transform.position - item.position;
            item.position += guideDirection.normalized * guideStrength; //move the object towards the center of this guide

            if(Vector3.Distance(item.position, transform.position) < 0.5f)
            {
                ReceiveObject(item); //run the receive function on this item to accept or reject it
            }
        }
    }
    void ReceiveObject(Transform item)
    {
        containedItems.Remove(item); //remvoe the item from the guiding list

        if(sortList.Contains(item.GetComponent<ItemController>().myCategory)) //if the incoming object is from a category we want
        {
            Debug.Log("Good");
            item.parent = storedParent; //store the item in this receptacle
            item.gameObject.SetActive(false); //disable the item
            
            faceAn.Play("binFaceCorrectAnim");
            //animate happy effects
            particles.SetActive(false);
            particles.SetActive(true); //turn the particles off and on again

            manager.AcceptItem();
            //call the manager to run a check to see if there are no more objects and therefore end the minigame

        }
        else
        {
            Debug.Log("Bad");
            faceAn.Play("binFaceWrongAnswer");
            binAn.Play("BinSlam");
            //somehow throw the item back onto the conveyor belt
            StartCoroutine(manager.RejectItem(item));
            //and set it to be kinematic again
            //put the item back under the 'belt' parent
        }

    }
}
}