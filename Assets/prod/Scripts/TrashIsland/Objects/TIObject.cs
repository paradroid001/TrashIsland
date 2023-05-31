using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{
    public class TIObject : MonoBehaviour, ITIObject
    {
        public virtual string GetObjectName()
        {
            return "Unknown Object";
        }
        public virtual string GetObjectDescription()
        {
            return "No description.";
        }

        public virtual IInteractable GetInteractable()
        {
            return null;
        }

        public virtual ISelectable GetSelectable()
        {
            return null;
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }
    }
}