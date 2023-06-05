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

        public virtual void OnSelected()
        {
            Debug.Log($"Item was selected: {name}");
            isSelected = true;

            renderer.SetPropertyBlock(selectedPropertyBlock);
        }
        public virtual void OnUnselected()
        {
            Debug.Log($"Item was unselected: {name}");
            isSelected = false;

            renderer.SetPropertyBlock(deselectedPropertyBlock);
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

            if (deselectedPropertyBlock == null)
                deselectedPropertyBlock = new MaterialPropertyBlock();
            deselectedPropertyBlock.SetFloat("_OutlineStrength", 0.0f);
            deselectedPropertyBlock.SetFloat("_OutlineWidth", 5.0f);

            //get the original settings
            //renderer.GetPropertyBlock(deselectedPropertyBlock);

            if (selectedPropertyBlock == null)
                selectedPropertyBlock = new MaterialPropertyBlock();
            selectedPropertyBlock.SetFloat("_OutlineStrength", outlineStrength);
            selectedPropertyBlock.SetColor("_OutlineColour", outlineColour);

        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}