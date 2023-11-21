using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using Yarn.Unity;

namespace TrashIsland
{
public class TempMovement : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField]
    private bool startInDebugMode;


    [Header("Movement")]
    [Space(5)]

    [SerializeField]
    private float movementSpeed = 0.5f;
    public float maxSpeed;
    public float rotationSpeed = 1f;
    public bool canMove;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    public float currentSpeed;
    [Space(20)]

    [Header("References")]
    [Space(5)]

    public GameObject playerBody;
    public Animator anim;

    public bool allowSelection = true;
    public bool movementOverride;

    [SerializeField]
    private LayerMask Interactables;

    [Space(20)]

    [Header("Dialogue")]
    [Space(5)]

    [SerializeField]
    private int dialogueCounter;
    [SerializeField] private List<string> dialogueTitles;
    public string activeNode;
    
    private Demo_InteractableNPC TalkingTo;
    private Demo_InteractableObject InteractingWith;


    

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        //rb = GetComponent<Rigidbody>();
        if (startInDebugMode)
        {
            SetNode("Admin");
            EnterDialogue();
            
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
        anim.SetFloat("speed", currentSpeed);

        if (canMove)
        {
            currentSpeed = moveDirection.magnitude;
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        
            moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

            //transform.position += moveDirection * movementSpeed * Time.deltaTime;
            transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.World);

            if (moveDirection != Vector3.zero)
            {
            //transform.forward = moveDirection;

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            currentSpeed = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GetMousePositionInWorld();               
        }
    }

    [YarnCommand("PassNode")]
    public void SetNode(string nodeName)
    {
        activeNode = nodeName;
    }

    public void ChangeLocation(Transform newPosition)
    {
        transform.position = newPosition.position;
    }

    public void DeleteObject()
    {
        Debug.Log("Deleting engaged");
        if(InteractingWith!=null)
        {            
            GameObject doomedObject = InteractingWith.gameObject;
            Debug.Log("Deleting: "+ doomedObject);
            ExitDialogue();
            
            Destroy(doomedObject);
        }   
    }

    public void EnterDialogue()
    {
        allowSelection = false;
        if(canMove)
        {
            DisableMovement();
            DialogueRunner r = FindObjectOfType<DialogueRunner>();
            if (r != null)
            {
                if (r.IsDialogueRunning)
                {
                    //If dialogue is already running, advance it.
                    LineView l = FindObjectOfType<LineView>();
                    if (l != null)
                    {
                        l.OnContinueClicked();
                    }
                }
                else
                {
                    
                    if (TalkingTo != null)
                    {
                        TalkingTo.BecomeSelected();
                    }

                    r.StartDialogue(activeNode);    
                }
            }
        }

    }
    public void ExitDialogue()
    {
        if(!canMove)
        {
            EnableMovement();
        }
        allowSelection = true;

        if (TalkingTo != null)
        {
            TalkingTo.BecomeDeselected();
        }
        
    }
    [YarnCommand("DelayTime")]
    public IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(3);
        movementOverride = false;
        canMove = true;
    }
    public void DisableMovement()
    {
        Debug.Log("Movement Disabled");
        canMove = false;
    }
    public void EnableMovement()
    {
        if (!movementOverride)
            canMove = true;
    }


    void GetMousePositionInWorld()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f, Interactables))
            { 
                GameObject g;
                Rigidbody rb = hit.collider.attachedRigidbody;
                if (rb == null)
                    g = hit.collider.gameObject;
                else
                    g = rb.gameObject;
                
            DemoYarnCommand dYC = FindObjectOfType<DemoYarnCommand>();
            if (dYC != null)
            {
                    Demo_InteractableNPC o = g.GetComponent<Demo_InteractableNPC>(); //Checks for NPC Script
                    if (o != null) //If we selected an NPC
                    {                        
                        if(allowSelection && o.IsInteractable)
                        {                            
                            TalkingTo = o;                       
                            EnterDialogue();      
                        }
                    }
                
                    Demo_InteractableObject i = g.GetComponent<Demo_InteractableObject>(); //Checks for Object Script
                    if (i != null) //If we selected an Interactable Object
                    {
                        if(allowSelection)
                        {
                            i.BecomeSelected();
                            InteractingWith = i;
                        }
                    }
            }
            }
            
        }

        [YarnCommand("playerAnim")]
        public void CallAnimation(string animName)
        {
            anim.Play(animName);
        }

        /*
        public void AdvanceDialogue()
        {
            dialogueCounter++;
        }
        */        
}
}
