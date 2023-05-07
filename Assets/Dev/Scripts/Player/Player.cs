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
    public bool acting;
    public List<GameObject> Interactables;
    public int currentInteract;
    public Transform hold;
    public ToolScriptableObject equippedTool;
    public DialogueUI dialogueUI;
    public GridDetector detector;
    public GameObject playerCanvas;
    public GameObject interactButtonTemplate;
    public GameObject interacting;
    public Animator anim;
    void Start()
    {
        //hold = transform.GetChild(0);
        anim = transform.GetComponent<Animator>();
    }
    void Update()
    {
        vert = Input.GetAxis("Vertical");
        anim.SetFloat("Y", vert);
        hori = Input.GetAxis("Horizontal");
        anim.SetFloat("X", hori);
        dir = new Vector3(hori, 0, vert);
        if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Pickup")
        {
            if(vert != 0 || hori != 0)
            {
                transform.Translate(dir * speed * Time.deltaTime, Space.World);
            }
            if (dir != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpd * Time.deltaTime);
            }
        }
        
        /*if(Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }*/
    }
    public void Interact()
    {
        if(interacting == null)
        {
            interacting = Interactables[currentInteract];
        }
                Debug.Log("R");
                
                if (Interactables[currentInteract].GetComponent<Trash>() != null || interacting.GetComponent<Trash>())
                {
                    Trash trash = Interactables[currentInteract].GetComponent<Trash>();
                    holding = false;
                    trash.Interact(this);
                }
                else if (interacting.GetComponent<Recycle>())
                {
                    Recycle recycle;
                    
                    //Trash trash = hold.GetChild(0).GetComponent<Trash>();
                    if(interacting == null)
                    {
                        recycle = Interactables[currentInteract].GetComponent<Recycle>();
                        recycle.interact = true;
                    }
                    else 
                    {
                        recycle = interacting.GetComponent<Recycle>();
                        recycle.interact = true;
                    }
                    holding = false;
                    recycle.Interact();
                    //Destroy(hold.GetChild(0).gameObject);
                }
                else if(interacting.GetComponent<Garbage>())
                {
                    Garbage garbage1;
                    if(interacting == null)
                    {
                        garbage1 = Interactables[currentInteract].GetComponent<Garbage>();
                        garbage1.interact = true;
                    }
                    else 
                    {
                        garbage1 = interacting.GetComponent<Garbage>();
                        garbage1.interact = true;
                    }
                    //Trash trash = hold.GetChild(0).GetComponent<Trash>();
                    //Garbage garbage = Interactables[currentInteract].GetComponent<Garbage>();
                    garbage1.Interact();
                    //Destroy(hold.GetChild(0).gameObject);
                }
                else if(interacting.GetComponent<Organics>())
                {
                    Organics organics;
                    if(interacting == null)
                    {
                        organics = Interactables[currentInteract].GetComponent<Organics>();
                        organics.interact = true;
                    }
                    else 
                    {
                        organics = interacting.GetComponent<Organics>();
                        organics.interact = true;
                    }
                    //Trash trash = hold.GetChild(0).GetComponent<Trash>();
                    //Organics organics = Interactables[currentInteract].GetComponent<Organics>();
                    organics.Interact();
                    //Destroy(hold.GetChild(0).gameObject);
                }
                else if(interacting.GetComponent<PickupEvent>())
                {
                    PickupEvent pickup;
                    if(interacting == null)
                    {
                        pickup = Interactables[currentInteract].GetComponent<PickupEvent>();
                    }
                    else 
                    {
                        pickup = interacting.GetComponent<PickupEvent>();
                    }
                    pickup.Interact();
                }
                else if(interacting.GetComponent<CraftingManager>())
                {
                    CraftingManager manager;
                    if(interacting == null)
                    {
                        manager = Interactables[currentInteract].GetComponent<CraftingManager>();
                    }
                    else 
                    {
                        manager = interacting.GetComponent<CraftingManager>();
                    }
                    manager.Interact();
                }
                else if(interacting.GetComponent<ProductionMachine>())
                {
                    ProductionMachine machine;
                    if(interacting == null)
                    {
                        machine = Interactables[currentInteract].GetComponent<ProductionMachine>();
                    }
                    else 
                    {
                        machine = interacting.GetComponent<ProductionMachine>();
                    } 
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
                else if(interacting.GetComponent<RobotCompanion>())
                {
                    RobotCompanion robot;
                    if(interacting == null)
                    {
                        robot = interacting.GetComponent<RobotCompanion>();
                    }
                    else
                    {
                        robot = interacting.GetComponent<RobotCompanion>();
                    }
                    robot.Interact();
                }
                else if(interacting.GetComponent<Compost>())
                {
                    Compost compost;
                    if(interacting == null)
                    {
                        compost = Interactables[currentInteract].GetComponent<Compost>();
                    }
                    else 
                    {
                        compost = interacting.GetComponent<Compost>();
                    }
                    compost.Interact();
                }
                else if(interacting.GetComponent<GrowLand>())
                {
                    GrowLand land;
                    if(interacting == null)
                    {
                        land = Interactables[currentInteract].GetComponent<GrowLand>();
                    }
                    else 
                    {
                        land = interacting.GetComponent<GrowLand>();
                    }
                    
                    land.Interact();
                }
                else if(interacting.GetComponent<Crate>())
                {
                    
                }
                else if(interacting.GetComponent<TrashMound>())
                {
                    TrashMound mound;
                    if(interacting == null)
                    {
                        mound = Interactables[currentInteract].GetComponent<TrashMound>();
                    }
                    else 
                    {
                        mound = interacting.GetComponent<TrashMound>();
                    }
                    mound.Interact();
                }
                else if(interacting.GetComponent<DialogueActivator>())
                {
                    DialogueActivator activator;
                    if(interacting == null)
                    {
                        activator = Interactables[currentInteract].GetComponent<DialogueActivator>();
                        
                    }
                    else 
                    {
                        activator = interacting.GetComponent<DialogueActivator>();
                    }
                    activator.Interact(GameManager.instance.player);
                }
                else if(interacting.GetComponent<Closet>())
                {
                    Closet closet = Interactables[currentInteract].GetComponent<Closet>();
                    closet.Interact();
                }
                else if(interacting.GetComponent<EnterBuildings>())
                {
                    EnterBuildings enter = interacting.GetComponent<EnterBuildings>();
                    enter.Interact();
                }
                interacting = null;
    }
}
