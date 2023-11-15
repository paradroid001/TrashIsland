using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{
public class TempMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 0.5f;
    public float maxSpeed;
    public float rotationSpeed = 1f;
    Rigidbody rb;
    public bool canMove;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    public float currentSpeed;
    public GameObject playerBody;
    public Animator anim;

    public bool allowSelection = true;

    [SerializeField]
    private LayerMask playerLayer;

    

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
        
        
        

        

        

        
        
        

        /*
        if (Input.anyKey == false)
        {
            
        }
        */
        
    }

    public void ChangeLocation(Transform newPosition)
    {
        transform.position = newPosition.position;
    }

    public void DisableMovement()
    {
        canMove = false;
    }
    public void EnableMovement()
    {
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
                        Debug.Log("hit");
                        if(allowSelection)
                        {
                        Debug.Log("Selected");

                        o.BecomeSelected();                    
                        }
                    }
            }
            if (Physics.Raycast(ray, out hit, 1000f, 4))
            {
                {
                        Debug.Log("Deselected");
                        TISelectionEvent e = new TISelectionEvent();
                        //e.selectableObject = deselect;
                        e.Call();
                        }
            }
        }
}
}
