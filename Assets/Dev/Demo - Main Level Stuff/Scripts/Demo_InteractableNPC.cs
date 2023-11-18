using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland 
{

public class Demo_InteractableNPC : MonoBehaviour
{
    [SerializeField]
    protected Collider interactionCollider;

    [SerializeField]
    Renderer myRenderer;
    
    List<TIInteractor> interactorsInRange = new List<TIInteractor>();
    public bool IsInteractable;
    [SerializeField]
    private bool isMoving;
    private bool IsSelected;

    private MaterialPropertyBlock deselectedPropertyBlock;
    private MaterialPropertyBlock selectedPropertyBlock;

    [SerializeField]
    private Transform NPCBody;
    [SerializeField]
    private bool looksAtPlayer;
    private bool resetGaze;
    private Quaternion defaultLookDirection;
    [SerializeField]
    private float staringSpeed;
    private float resetTimer;
    private float timesLooped;


    [Header("Outline Settings")]
    [SerializeField]
    private Material selectionMaterial;

    [SerializeField]
    [Range(.002f, 0.2f)]
    private float outlineStrength;
    [SerializeField]
    private Color outlineColour;
    [SerializeField] 
    private Color selectedColour;

    private GameObject player;

    [Space(20)]

    [Header("Dialogue Settings")]
    [SerializeField]
    private int interactionCount;
    public bool hasMovementAnimation;

    protected int FindIndexOfMaterial(Material m)
        {
            if (m == null)
                return -1;

            Material[] mats = myRenderer.materials;

            for (int i = 0; i < mats.Length; i++)
            {
                //Compare names. It helps with 'instanced' materials
                if (mats[i].name == m.name || mats[i].name == m.name + " (Instance)")
                {
                    return i;
                }
            }
            return -1;
        }

    // HasNewDialogue - perhaps outline colour could change to indicate new dialogue available?

    void Start()
    {
        
        if (myRenderer == null)
            {
                myRenderer = GetComponent<Renderer>();
            }

        defaultLookDirection = NPCBody.transform.rotation;
    }
    void Update()
    {
        if (looksAtPlayer && IsInteractable) // If the player is in range, look at them
        {
            Vector3 relativePosition = player.transform.position - transform.position;


            Quaternion newRotation = Quaternion.LookRotation(relativePosition, Vector3.up);

            NPCBody.transform.rotation = Quaternion.Lerp(NPCBody.transform.rotation, newRotation, Time.deltaTime * staringSpeed);
        }

        if (looksAtPlayer && resetGaze && !isMoving && !IsInteractable)
        {
                     
            resetTimer += Time.deltaTime;
            if (resetTimer >= 2)
            {
                resetTimer = 0;
                timesLooped++;
            }

            if (timesLooped <= 0) // Continues starting at player a short time after they've left the interaction zone
            {
                Vector3 relativePosition = player.transform.position - transform.position;
                Quaternion newRotation = Quaternion.LookRotation(relativePosition, Vector3.up);
                NPCBody.transform.rotation = Quaternion.Lerp(NPCBody.transform.rotation, newRotation, Time.deltaTime * staringSpeed);
            }
            else
            {        
                NPCBody.transform.rotation = Quaternion.Lerp(NPCBody.transform.rotation, defaultLookDirection, Time.deltaTime * staringSpeed);
            } 

            

            if (timesLooped>=2)
            {
                resetGaze = false;
                timesLooped = 0;
                resetTimer = 0;
            }
        }


    }

    void BecomeInteractable()
    {        
        IsInteractable = true;
        //Set outline
        //Material selectionMaterial = objectTemplate.selectableConfig.selectionMaterial;
            int index = FindIndexOfMaterial(selectionMaterial);
            if (index < 0)
            {
            //Copy out old mats
                Material[] mats = myRenderer.materials;
                Material[] newmats = new Material[mats.Length + 1];
                for (int i = 0; i < mats.Length; i++)
                {
                    newmats[i] = mats[i];
                }
                //Add the semection mat on the end
                newmats[newmats.Length - 1] = selectionMaterial;
                selectionMaterial.SetColor("_OutlineColor", outlineColour);
                selectionMaterial.SetFloat("_Outline", outlineStrength);
                //Set the whole array back onto the renderer
                myRenderer.materials = newmats;
                }
                myRenderer.SetPropertyBlock(selectedPropertyBlock);
    }

    void BecomeUninteractable()
    {
        IsInteractable = false;
        //Disable/reset outline

        if (myRenderer != null)
            {
                //Set the property block first, in case removing mat fails.
                myRenderer.SetPropertyBlock(deselectedPropertyBlock);                
                //Remove the material
                int index = FindIndexOfMaterial(selectionMaterial);
                if (index > 0)
                {
                    //Copy out old mats
                    Material[] mats = myRenderer.materials;
                    Material[] newmats = new Material[mats.Length - 1];
                    int nextindex = 0;
                    for (int i = 0; i < mats.Length; i++)
                    {
                        if (i != index)
                        {
                            newmats[nextindex] = mats[i];
                            nextindex += 1;
                        }

                    }
                    //Set the whole array back onto the renderer
                    myRenderer.materials = newmats;
                }
            }
    }

    public void BecomeSelected()
    { 
        if (IsInteractable)
        {
            IsSelected = true;

            if(myRenderer != null)
            {
                int materialCount = myRenderer.materials.Length - 1;
                myRenderer.materials[materialCount].SetColor("_OutlineColor", selectedColour);
            }
        }
        
    }

    public void BecomeDeselected()
    {
        IsSelected = false;

        if(myRenderer != null && IsInteractable)
        {
            int materialCount = myRenderer.materials.Length - 1;
            myRenderer.materials[materialCount].SetColor("_OutlineColor", outlineColour);
        }
    }
    
    public void InMotion()
    {
        isMoving = true;
        BecomeUninteractable();

        Animator a = GetComponent<Animator>();
        if (hasMovementAnimation && a != null)
        {
            a.SetTrigger("isMoving");
        }
    }
    public void EndMotion()
    {
        isMoving = false;

        float distance = GetPlayerDistance();
        if (distance <= 3.45f)
        {
            BecomeInteractable();
        }
        

        Animator a = GetComponent<Animator>();
        if (hasMovementAnimation && a != null)
        {
            a.SetTrigger("isNotMoving");
        }
    }


    float GetPlayerDistance()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return distance;
    }
    void OnTriggerEnter(Collider c)
        {
            TIInteractor t = c.GetComponent<TIInteractor>();            
            if (t != null && !isMoving) //if the player is an interactor and we are't moving
            {
                player = t.gameObject;
                if (!interactorsInRange.Contains(t))
                {
                    interactorsInRange.Add(t);
                    
                    //t.AddInteractable(this);
                    BecomeInteractable();
                }
            }
        }
        
        void OnTriggerExit(Collider c)
        {
            TIInteractor t = c.GetComponent<TIInteractor>();
            if (t != null && !isMoving)
            {
                if (interactorsInRange.Contains(t))
                {
                    interactorsInRange.Remove(t);
                    //t.RemoveInteractable(this);
                    BecomeUninteractable();
                    if (looksAtPlayer)
                    {
                        resetGaze = true;
                    }
                }
            }
        } 
}
}
