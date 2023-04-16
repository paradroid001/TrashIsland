using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotSpd;
    public float vert;
    public float hori;
    public Vector3 dir;
    public bool interactable;
    public bool holding;
    public List<GameObject> Interactables;
    public int currentInteract;
    public Transform hold;
    public ToolScriptableObject equippedTool;
    public DialogueUI dialogueUI;
    public GridDetector detector;
    void Start()
    {
        //hold = transform.GetChild(0);
    }
    void Update()
    {
        vert = Input.GetAxis("Vertical");
        hori = Input.GetAxis("Horizontal");
        dir = new Vector3(hori, 0, vert);
        if(vert != 0 || hori != 0)
        {
            transform.Translate(dir * speed * Time.deltaTime, Space.World);
        }
        if (dir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpd * Time.deltaTime);
        }
        if(interactable)
        {
            Interact();
        }
    }
    public void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E))
            {
                if (Interactables[currentInteract].GetComponent<Trash>() != null)
                {
                    Trash trash = Interactables[currentInteract].GetComponent<Trash>();
                    holding = false;
                    trash.Interact(this);
                }
                else if (Interactables[currentInteract].GetComponent<Recycle>() != null)
                {
                    //Trash trash = hold.GetChild(0).GetComponent<Trash>();
                    Recycle recycle = Interactables[currentInteract].GetComponent<Recycle>();
                    holding = false;
                    recycle.Interact();
                    //Destroy(hold.GetChild(0).gameObject);
                }
                else if(Interactables[currentInteract].GetComponent<Garbage>() != null)
                {
                    //Trash trash = hold.GetChild(0).GetComponent<Trash>();
                    Garbage garbage = Interactables[currentInteract].GetComponent<Garbage>();
                    garbage.Interact();
                    //Destroy(hold.GetChild(0).gameObject);
                }
                else if(Interactables[currentInteract].GetComponent<Organics>() != null)
                {
                    //Trash trash = hold.GetChild(0).GetComponent<Trash>();
                    Organics organics = Interactables[currentInteract].GetComponent<Organics>();
                    organics.Interact();
                    //Destroy(hold.GetChild(0).gameObject);
                }
                else if(Interactables[currentInteract].GetComponent<PickupEvent>() != null)
                {
                    PickupEvent pickup = Interactables[currentInteract].GetComponent<PickupEvent>();
                    pickup.Interact();
                }
                else if(Interactables[currentInteract].GetComponent<CraftingManager>() != null)
                {
                    CraftingManager manager = Interactables[currentInteract].GetComponent<CraftingManager>();
                    manager.Interact();
                }
                else if(Interactables[currentInteract].GetComponent<ProductionMachine>() != null)
                {
                    ProductionMachine machine = Interactables[currentInteract].GetComponent<ProductionMachine>();
                    if(machine.contains.Capacity != 0)
                    {
                        StartCoroutine(machine.Interact());
                    }
                    else if(machine.contains.Capacity == 0)
                    {
                        GameManager.instance.invent.currentMachine = machine;
                        GameManager.instance.invent.reason = Inventory.InventoryReason.Process;
                    }
                }
                else if(Interactables[currentInteract].GetComponent<RobotCompanion>() != null)
                {
                    RobotCompanion robot = Interactables[currentInteract].GetComponent<RobotCompanion>();
                    robot.Interact();
                }
                else if(Interactables[currentInteract].GetComponent<Compost>() != null)
                {
                    Compost compost = Interactables[currentInteract].GetComponent<Compost>();
                    compost.Interact();
                }
                else if(Interactables[currentInteract].GetComponent<GrowLand>())
                {
                    GrowLand land = Interactables[currentInteract].GetComponent<GrowLand>();
                    land.Interact();
                }
                else if(Interactables[currentInteract].GetComponent<Crate>())
                {
                    
                }
                else if(Interactables[currentInteract].GetComponent<TrashMound>())
                {
                    TrashMound mound = Interactables[currentInteract].GetComponent<TrashMound>();
                    mound.Interact();
                }
                else if(Interactables[currentInteract].GetComponent<DialogueActivator>())
                {
                    DialogueActivator activator = Interactables[currentInteract].GetComponent<DialogueActivator>();
                    activator.Interact(this);
                }
                else if(Interactables[currentInteract].GetComponent<Closet>())
                {
                    Closet closet = Interactables[currentInteract].GetComponent<Closet>();
                    closet.Interact();
                }
            }
    }
}
