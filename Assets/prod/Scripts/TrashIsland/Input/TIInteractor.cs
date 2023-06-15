using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{
    public class TIInteractor : MonoBehaviour, IInteractor
    {
        public bool AddInteractable(IInteractable interactable)
        {
            //do nothing
            return true;
        }

        public List<IInteractable> GetInteractables()
        {
            return new List<IInteractable>();
        }

        public bool RemoveInteractable(IInteractable interactable)
        {
            return true;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}