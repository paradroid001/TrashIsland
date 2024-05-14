using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland 
{
public class JDemo_Machines : MonoBehaviour
{
    enum MachineType {Shredder, Roller, Crusher}

    [Header("Timer Variables")]
    [SerializeField]
    private float timerLength;
    private float uptime;
    private bool isActive;

    [Space(20)]

    [Header("Item Variables")]
    [SerializeField]
    private string requiredItemName;
    [SerializeField]
    private string outputItemName;
    [SerializeField]
    private int outputItemID;
    [SerializeField]
    private bool hasCollectableItem;

    [Space(20)]
    [Header("Obtained Player Information")]
    [SerializeField]
    private JDemo_Inventory pInvRef;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && !hasCollectableItem)
        {
            uptime+=Time.deltaTime;

            if (uptime >= timerLength)
            {
                hasCollectableItem = true;
                Debug.Log(gameObject.name+": Processing Finished");
                GetComponent<Demo_InteractableObject>().ActivateObjectInteraction();
                isActive = false;
            }
        }
    }

    public void InteractWithMachine()
    {
        if (isActive)
        {
            Debug.Log("Error: Machine is already running");
        }
        else
        {
            if (pInvRef != null)
            {
                // If player has an item and machine does not already have an item ready to collect
                if (pInvRef.playerHasItem && !hasCollectableItem)
                {
                    // Gather the name and ID of item held from our player's inventory script
                    string itemNameGathered = pInvRef.heldItemName;
                    int itemIDGathered = pInvRef.heldItemID;

                    AttemptMachineProcess(itemNameGathered, pInvRef);
                }
                else
                {
                    // If machine has an item ready to collect and player has room in inventory to recieve it
                    if (hasCollectableItem && !pInvRef.playerHasItem)
                    {
                        bool hasItem = pInvRef.playerHasItem;
                        AttemptRetrieveItem(hasItem, pInvRef);
                    }
                    else
                    {
                        Debug.Log("Error: Player has no item and "+gameObject.name+" has no collectables");
                    }
                }
                

            }
            else
            {
                Debug.Log("Error - no player inventory reference available to gather from");
            }           
        }
    }

    public void AttemptMachineProcess(string heldItemName, JDemo_Inventory pInvRef)
    {
        if (heldItemName == requiredItemName)
        {
            Debug.Log("Success! Processing Item");
            pInvRef.ClearInventory();
            GetComponent<Demo_InteractableObject>().DeactivateInt();
            uptime = 0;
            isActive = true;
        }
        else 
        {
            Debug.Log("Missing Required Item For Machine Type (requires: " + requiredItemName);
        }
    }

    public void AttemptRetrieveItem(bool hasHeldItem, JDemo_Inventory pInventory)
    {
        if (!hasHeldItem && hasCollectableItem)
        {
            pInventory.AttemptAddItem(outputItemName, outputItemID, /* int progression */);

            hasCollectableItem = false;

            GetComponent<Demo_InteractableObject>().DeactivateInt();
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<JDemo_Inventory>() != null)
        {
            pInvRef = c.gameObject.GetComponent<JDemo_Inventory>();
        }
    }

    void OnTriggerExit(Collider c) 
    {
        if (pInvRef!=null)
        {
            pInvRef = null;
        }
    }
}
}
