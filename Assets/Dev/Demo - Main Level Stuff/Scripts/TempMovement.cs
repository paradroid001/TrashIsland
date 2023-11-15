using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using Yarn.Unity;

namespace TrashIsland
{
public class TempMovement : MonoBehaviour
{
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
    private LayerMask playerLayer;

    [Space(20)]

    [Header("Dialogue")]
    [Space(5)]

    [SerializeField]
    private int dialogueCounter;
    [SerializeField] private List<string> dialogueTitles;
    public string activeNode;
    [SerializeField]
    private Demo_InteractableNPC TalkingTo;
    [SerializeField]
    private Demo_InteractableObject InteractingWith;


    

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        canMove = true;
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
                    // Kick off the dialogue at this node.
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
            if (Physics.Raycast(ray, out hit, 1000f, ~playerLayer))
            { 
                GameObject g;
                Rigidbody rb = hit.collider.attachedRigidbody;
                if (rb == null)
                    g = hit.collider.gameObject;
                else
                    g = rb.gameObject;

            Demo_InteractableNPC o = g.GetComponent<Demo_InteractableNPC>();
                    if (o != null)
                    {                        
                        if(allowSelection && o.IsInteractable)
                        {
                        o.BecomeSelected();
                        TalkingTo = o;
                        EnterDialogue();                    
                        }
                    }
            Demo_InteractableObject i = g.GetComponent<Demo_InteractableObject>();
                if (i != null)
                {
                    if(allowSelection)
                        {
                        movementOverride = true;
                        i.BecomeSelected();
                        InteractingWith = i;
                        
                                     
                        }
                }
            }
            
        }

        [YarnCommand("playAnim")]
        public void CallAnimation(string animName)
        {
            anim.Play(animName);
        }

        public void AdvanceDialogue()
        {
            dialogueCounter++;
        }
}
}
