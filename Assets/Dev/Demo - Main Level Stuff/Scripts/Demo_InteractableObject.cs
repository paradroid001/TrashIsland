using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace TrashIsland 
{

public class Demo_InteractableObject : MonoBehaviour
{
    [SerializeField]
    private bool InteractionReady;


    [SerializeField]
    protected Collider interactionCollider;

    [SerializeField]
    Renderer myRenderer;
    
    List<TIInteractor> interactorsInRange = new List<TIInteractor>();
    public bool IsInteractable;

    private bool IsSelected;

    private MaterialPropertyBlock deselectedPropertyBlock;
    private MaterialPropertyBlock selectedPropertyBlock;

    [SerializeField]
    private string myNode;


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

    [Space(20)]

    [Header("Dialogue Settings")]
    [SerializeField]
    private int interactionCount;

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

    void StartObjectDialogue()
    {
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
                    r.StartDialogue(myNode);
                }
            }
    }

    [YarnCommand("ActivateObjectInteraction")]
    public void ActivateObjectInteraction()
    {
        InteractionReady = true;
    }
    

    void BecomeInteractable()
    {        
        IsInteractable = true;
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
            StartObjectDialogue();
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


    void OnTriggerEnter(Collider c)
        {

            TIInteractor t = c.GetComponent<TIInteractor>();
            if (t != null && InteractionReady)
            {
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
            if (t != null && InteractionReady)
            {
                if (interactorsInRange.Contains(t))
                {
                    interactorsInRange.Remove(t);
                    //t.RemoveInteractable(this);
                    BecomeUninteractable();
                }
            }
        } 

        
}
}

