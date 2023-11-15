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

    private bool IsSelected;

    private MaterialPropertyBlock deselectedPropertyBlock;
    private MaterialPropertyBlock selectedPropertyBlock;


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

    //[Space(20)]

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

    // Start is called before the first frame update
    void Start()
    {
        if (myRenderer == null)
            {
                myRenderer = GetComponent<Renderer>();
            }
    }

    // Update is called once per frame
    void Update()
    {
        
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


    void OnTriggerEnter(Collider c)
        {
            TIInteractor t = c.GetComponent<TIInteractor>();
            if (t != null)
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
            if (t != null)
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
