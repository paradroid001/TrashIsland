using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
    public class TISelectableObject : TIObject, ISelectable
    {
        [SerializeField]
        protected new Renderer renderer;

        [SerializeField]
        protected TISelectableObject selectable;

        protected bool isSelected = false;
        [SerializeField]
        protected float outlineStrength = 7.0f;
        [SerializeField]
        protected Color outlineColour = Color.white;
        //The material property block we pass to the GPU
        private MaterialPropertyBlock deselectedPropertyBlock;
        private MaterialPropertyBlock selectedPropertyBlock;

        protected int FindIndexOfMaterial(Material m)
        {
            if (m == null)
                return -1;

            Material[] mats = renderer.materials;

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

        protected TISelectableConfigData GetSelectableConfig()
        {
            if (objectTemplate != null)
                return objectTemplate.selectableConfig;
            return null;
        }

        public virtual void OnSelected()
        {
            Debug.Log($"Item was selected: {name}");
            isSelected = true;

            if (renderer != null && objectTemplate != null)
            {
                Material selectionMaterial = objectTemplate.selectableConfig.selectionMaterial;
                int index = FindIndexOfMaterial(selectionMaterial);
                if (index < 0)
                {
                    //Copy out old mats
                    Material[] mats = renderer.materials;
                    Material[] newmats = new Material[mats.Length + 1];
                    for (int i = 0; i < mats.Length; i++)
                    {
                        newmats[i] = mats[i];
                    }
                    //Add the semection mat on the end
                    newmats[newmats.Length - 1] = selectionMaterial;
                    //Set the whole array back onto the renderer
                    renderer.materials = newmats;
                }
                renderer.SetPropertyBlock(selectedPropertyBlock);
            }
        }
        public virtual void OnUnselected()
        {
            Debug.Log($"Item was unselected: {name}");
            isSelected = false;

            if (renderer != null && objectTemplate != null)
            {
                //Set the property block first, in case removing mat fails.
                renderer.SetPropertyBlock(deselectedPropertyBlock);
                Material selectionMaterial = objectTemplate.selectableConfig.selectionMaterial;
                //Remove the material
                int index = FindIndexOfMaterial(selectionMaterial);
                if (index > 0)
                {
                    //Copy out old mats
                    Material[] mats = renderer.materials;
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
                    renderer.materials = newmats;
                }
            }

        }
        public virtual bool IsAvailableForSelection()
        {
            return true;
        }

        public virtual bool IsSelected()
        {
            return isSelected;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            if (renderer == null)
            {
                renderer = GetComponent<Renderer>();
            }
            selectable = this;


            //NOTE
            //PROPERTY BLOCKS must match your shaders.
            //Change the material used for selection? YES?
            //Does it have a different shader? Need to check the exposed vars.
            //AND pass in values that make sense (i.e. in range);

            if (deselectedPropertyBlock == null)
                deselectedPropertyBlock = new MaterialPropertyBlock();
            //Colour doesn't matter
            deselectedPropertyBlock.SetFloat("_Outline", 0.0f);
            deselectedPropertyBlock.SetFloat("_OutlineZ", 0.0f);

            //get the original settings
            //renderer.GetPropertyBlock(deselectedPropertyBlock);

            if (selectedPropertyBlock == null)
                selectedPropertyBlock = new MaterialPropertyBlock();
            TISelectableConfigData selectableConfig = GetSelectableConfig();
            if (selectableConfig != null)
            {
                selectedPropertyBlock.SetFloat("_Outline", selectableConfig.outlineStrength);
                selectedPropertyBlock.SetColor("_OutlineColor", selectableConfig.outlineColour);
                //selectedPropertyBlock.SetFloat("_OutlineWidth", selectableConfig.outlineWidth);
            }
            else
            {
                selectedPropertyBlock.SetFloat("_Outline", outlineStrength);
                //selectedPropertyBlock.SetFloat("_OutlineWidth", 5.0f);
                selectedPropertyBlock.SetColor("_OutlineColor", outlineColour);
            }
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}