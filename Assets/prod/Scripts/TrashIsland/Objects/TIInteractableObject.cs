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
        public TIInteraction interaction;
    }

    public class TIInteractableObject : TISelectableObject, IInteractable
    {
        [SerializeField]
        protected TIInteractableObject interactable;

        [SerializeField]
        protected Collider interactionCollider;

        [SerializeField]
        protected TIInteractionDef[] interactions;

        Dictionary<int, TIInteractionDef> interactionDict;

        List<TIInteractor> interactorsInRange = new List<TIInteractor>();

        protected bool isInteractable;

        public virtual bool IsInteractable()
        {
            return isInteractable;
        }

        public void BecomeInteractable()
        {
            //throw new System.NotImplementedException();
            isInteractable = true;
            Debug.Log($"{name} became interactable");
        }

        public void BecomeUninteractable()
        {
            //throw new System.NotImplementedException();
            isInteractable = false;
            Debug.Log($"{name} became uninteractable");
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
            if (interactionDict.Count > 0)
            {
                return interactionDict[0].interaction.state;
            }
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
            interactionDict = new Dictionary<int, TIInteractionDef>();
            //Build the dict
            for (int i = 0; i < interactions.Length; i++)
            {
                TIInteractionDef id = interactions[i];
                if (interactionDict.ContainsKey(id.interactionID))
                {
                    Debug.LogError($"There are duplicate interaction ids in {name}");
                }
                interactionDict[id.interactionID] = id;
            }
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        protected virtual void OnTriggerEnter(Collider c)
        {
            TIInteractor t = c.GetComponent<TIInteractor>();
            if (t != null)
            {
                if (!interactorsInRange.Contains(t))
                {
                    interactorsInRange.Add(t);
                    t.AddInteractable(this);
                    BecomeInteractable();
                }
            }
        }
        protected virtual void OnTriggerExit(Collider c)
        {
            TIInteractor t = c.GetComponent<TIInteractor>();
            if (t != null)
            {
                if (interactorsInRange.Contains(t))
                {
                    interactorsInRange.Remove(t);
                    t.RemoveInteractable(this);
                    BecomeUninteractable();
                }
            }
        }
    }
}