using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{
    [System.Serializable]
    public class TISelectionEvent : GameEvent<TISelectionEvent>
    {
        public ISelectable selectableObject;
    }

    public class TISelectionTracker : MonoBehaviour
    {
        ISelectable currentSelection;
        ISelectable previousSelection;
        // Start is called before the first frame update
        void Start()
        {
            TISelectionEvent.Register(HandleTISelectionEvent);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnDestroy()
        {
            TISelectionEvent.Unregister(HandleTISelectionEvent);
        }

        void HandleTISelectionEvent(TISelectionEvent e)
        {
            if (currentSelection != e.selectableObject)
            {
                previousSelection = currentSelection;
                currentSelection = e.selectableObject;
                previousSelection?.OnUnselected();
                currentSelection?.OnSelected();
            }

        }
    }
}