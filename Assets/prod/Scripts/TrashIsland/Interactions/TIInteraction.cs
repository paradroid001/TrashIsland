using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameCore;

namespace TrashIsland
{

    public class TIInteraction : MonoBehaviour
    {
        public UnityEvent<TIInteraction, TIInteractor> OnInteractionStart = new UnityEvent<TIInteraction, TIInteractor>();
        //public UnityEvent<TIInteraction, IInteractor> OnInteractionRun = new UnityEvent<TIInteraction, IInteractor>();
        public UnityEvent<TIInteraction, TIInteractor> OnInteractionFinish = new UnityEvent<TIInteraction, TIInteractor>();

        public InteractionState state;

        // Start is called before the first frame update
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        public virtual void Interact(TIInteractor interactor)
        {

        }
    }
}