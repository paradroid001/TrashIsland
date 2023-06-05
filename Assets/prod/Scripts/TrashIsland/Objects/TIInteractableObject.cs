using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;
using UnityEngine.Events;

namespace TrashIsland
{
    [System.Serializable]
    public class TIInteractionDef : InteractionDef
    {
        public UnityEvent<GameObject> action;
        public InteractionState state;
    }

    public class TIInteractableObject : TISelectableObject, IInteractable
    {
        [SerializeField]
        protected TIInteractableObject interactable;
        [SerializeField]
        protected TIInteractionDef[] interactions;

        public void BecomeInteractable()
        {
            //throw new System.NotImplementedException();
        }

        public void BecomeUninteractable()
        {
            //throw new System.NotImplementedException();
        }

        public bool CancelInteraction(IInteractor interactor, int interactionID)
        {
            //throw new System.NotImplementedException();
            return true;
        }

        public string GetInteractableName()
        {
            return _objectData.name;
        }

        public InteractionDef[] GetInteractions(IInteractor interactor)
        {
            return interactions;
        }

        public InteractionState GetInteractionState(IInteractor interactor, int interactionID)
        {
            return InteractionState.FINISHED;
        }

        public bool Interact(IInteractor interactor, int interactionID)
        {
            return false;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            interactable = this;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}