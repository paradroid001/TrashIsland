using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn;

namespace TrashIsland 
{

public class Demo_InteractableNPC : MonoBehaviour
{

    [Header("NPC Info")] 
    [Space(10)]   

    public string myName;
    public Sprite Icon;

    [SerializeField]
    private bool startsActive;
    [Space(5)]

    [SerializeField]
    private Transform NPCBody;

    [Space(5)]

    [SerializeField]
    protected Collider interactionCollider;
    
    private int _defaultLayer = 7;

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
    private bool looksAtPlayer;
    private bool resetGaze;
    private Quaternion defaultLookDirection;
    [SerializeField]
    private float staringSpeed;
    private float resetTimer;
    private float timesLooped;


    [Header("Outline Settings")]
    private bool _outlineEnabled;

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
    [Space(5)]
    public string startNode;

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

    void Awake()
    {
        
        if (myRenderer == null)
            {
                myRenderer = GetComponent<Renderer>();
            }

        defaultLookDirection = NPCBody.transform.rotation;

        DemoYarnCommand dYC = FindObjectOfType<DemoYarnCommand>();
        if (dYC != null)
        {
            dYC.activeNPCList.Add(this);
        }

        if (startNode == null)
        {
            Debug.Log("Error: Dialogue Node not set on "+ gameObject.name);
        }
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

    [YarnCommand("SetNPCName")]
    public void setName(string v) //Ensures that we have a consistent name string when passing variables to and from Yarn
    {
        Debug.Log("Setting "+ gameObject.name + "up for name");

        InMemoryVariableStorage varStor = FindObjectOfType<InMemoryVariableStorage>();
        string n;
        varStor.TryGetValue(v, out n);

        myName = n;

        if (!startsActive)
        {
            gameObject.SetActive(false);
        }
    }

    

    void BecomeInteractable()
    {        
        IsInteractable = true;
        gameObject.layer = 8; //Sets the game object layer to Interactable

        //Set outline

        SetOutlineActive(true);

        /*
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
        */
    }

    void BecomeUninteractable()
    {
        IsInteractable = false;
        gameObject.layer = _defaultLayer;
        //Disable/reset outline
        SetOutlineActive(false);

        
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

    [YarnCommand("ActivateOutline")]
    public void SetOutlineActive(bool b)
    {
        if (b)  // If true, we are calling to enable outline
        {
            if (_outlineEnabled)
            {
                return;
            }
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
                _outlineEnabled = true;
                //Set the whole array back onto the renderer
                myRenderer.materials = newmats;
            }
            myRenderer.SetPropertyBlock(selectedPropertyBlock);
        }
        else // If not, we are calling to disable it
        {
            if (myRenderer != null)
            {
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
                _outlineEnabled = false;
            }
        }
    }

    [YarnCommand("ForcedOutlineEnd")]
    public void CheckOutlineValid()
    {
        if (!IsInteractable)
        {
            SetOutlineActive(false);
        }
        else
        {
            SelectedOutlineOverride(false); //Sets the outline from selected colour back to normal colour if NPC is still in range
        }
    }

    public void SelectedOutlineOverride(bool b) // Essentially just used for yarn spinner to control selection colour based on who is talking 
    {
        if(myRenderer != null)
        {
            int materialCount = myRenderer.materials.Length - 1;
            if (b)
            {
                myRenderer.materials[materialCount].SetColor("_OutlineColor", selectedColour);
            }
            else
                myRenderer.materials[materialCount].SetColor("_OutlineColor", outlineColour);
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
        SetOutlineActive(false);
        gameObject.layer = _defaultLayer;
        looksAtPlayer = false;
        Animator a = GetComponent<Animator>();
        if (hasMovementAnimation && a != null)
        {
            a.SetTrigger("isMoving");
        }

        
    }
    public void EndMotion()
    {
        isMoving = false;
        looksAtPlayer = true;
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
            BecomeInteractable();
        }
    }
    bool inZone;
    private void OnTriggerStay(Collider c) 
    {
        if (inZone)
            return;
        else
        {
        TIInteractor t = c.GetComponent<TIInteractor>();            
        if (t != null && !isMoving) //if the player is an interactor and we are't moving
        {
            player = t.gameObject;
            if (!interactorsInRange.Contains(t))
            {
                interactorsInRange.Add(t);
                BecomeInteractable();                   
                //t.AddInteractable(this);            
            }
            inZone = true;
        }
        }
    }
    
        
    void OnTriggerExit(Collider c)
    {
        TIInteractor t = c.GetComponent<TIInteractor>();
        if (t != null && !isMoving)
        {
            BecomeUninteractable();
            if (looksAtPlayer)
            {
                resetGaze = true;
            }
            inZone = false;
            
        }
    } 
}
}
