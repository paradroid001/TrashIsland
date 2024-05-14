using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland 
{
public class JDemo_Machines : MonoBehaviour
{
    enum MachineType {Shredder, Roller, Recycler, Washer, Smelter}

    [SerializeField]
    MachineType machineType;

    [Header("Timer Variables")]
    [SerializeField]
    private float timerLength;
    private float uptime;
    private bool isActive;

    [Space(20)]

    [Header("Item Variables")]
    [SerializeField]
    private int storedID;
    [SerializeField]
    private int storedProg;
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
                UpdateItemStats();
                hasCollectableItem = true;
                Debug.Log(gameObject.name+": Processing Finished");
                GetComponent<Demo_InteractableObject>().ActivateObjectInteraction();
                isActive = false;
            }
        }
    }

    private void UpdateItemStats()
    {
        storedProg = storedProg+1;
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

        int passedID = pInvRef.heldItemID;
        int passedProg = pInvRef.heldItemProgression;
        string passedName = pInvRef.heldItemName;

        if (ValidateItemAcceptance(passedID, passedProg))
        {
            Debug.Log("Success! Processing Item");

            outputItemName = passedName;
            storedID = passedID;
            storedProg = passedProg;

            pInvRef.ClearInventory();
            GetComponent<Demo_InteractableObject>().DeactivateInt();
            uptime = 0;
            isActive = true;
        }
        else 
        {
            Debug.Log("Missing Required Item For Machine Type");
        }
    }

    public void AttemptRetrieveItem(bool hasHeldItem, JDemo_Inventory pInventory)
    {
        if (!hasHeldItem && hasCollectableItem)
        {
            pInventory.AttemptAddItem(outputItemName, storedID, storedProg);

            FindObjectOfType<NodeOverriderSPARC>().UpdateProgression();
            hasCollectableItem = false;

            outputItemName = null;
            storedID = 0;
            storedProg = 0;
            
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

    bool ValidateItemAcceptance(int itemID, int itemProgRef)
    {
        // 1 = Aluminium        
        // 2 = Paper
        // 3 = Glass
        // 4 = Plastic

        if (machineType == MachineType.Shredder)
        {
            if (itemID == 1) // if item is Aluminium
            {
                if(itemProgRef == 1) // in Trash Form
                {
                    return true;
                }
                else
                    return false;
            }

            if (itemID == 3) // if item is Glass
            {
                if(itemProgRef == 1) // in Trash Form
                {
                    return true;
                }
                else
                    return false;
            }
            else
                    return false;
        }        
        if (machineType == MachineType.Roller)
        {
            if (itemID == 2) // if item is Paper
            {
                if(itemProgRef == 3) // in Smelted Form
                {
                    return true;
                }
                else
                    return false;
            }
            else
                    return false;
        }
        if (machineType == MachineType.Recycler)
        {
            return false;
        }
        if (machineType == MachineType.Washer)
        {
            if(itemID == 1) // if item is Aluminium
            {
                if(itemProgRef == 2) // in washed form
                {
                    return true;
                }
                else
                    return false;
            }

            if(itemID == 2) // if item is Paper
            {
                if(itemProgRef == 1) // in trash form 
                {
                    return true;
                }
                else
                    return false;
            }

            if(itemID == 3) // if item is Glass
            {
                if(itemProgRef == 2) // in shredded form
                {
                    return true;
                }
                else
                    return false;
            }
            
            if(itemID == 4) // if item is Plastic
            {
                if(itemProgRef == 2) // in shredded form
                {
                    return true;
                }
                else
                    return false;
            }
            else
                    return false;
        }
        if (machineType == MachineType.Smelter)
        {
            if (itemID == 1) // if item is Aluminium
            {
                if(itemProgRef == 3) // in washed form
                {
                    return true;
                }
                else
                    return false;
            }

            if (itemID == 2) // if item is Paper
            {
                if(itemProgRef == 2) // in washed form
                {
                    return true;
                }
                else
                    return false;
            }
            if (itemID == 3) // if item is Glass
            {
                if (itemProgRef == 3) // in washed form
                {
                    return true;
                }
                else
                    return false;
            }
            if (itemID == 4) // if item is Plastic
            {
                if(itemProgRef == 3) // in washed form
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        else
        {
            return false;
        }
    }
}
}
