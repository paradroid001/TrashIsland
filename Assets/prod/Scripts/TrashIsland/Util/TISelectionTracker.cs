using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{
    [System.Serializable]
    public class TISelectionEvent : GameEvent<TISelectionEvent>
    {
        public TISelectableObject selectableObject;
    }

    public class TISelectionTracker : MonoBehaviour
    {
        TISelectableObject currentSelection;
        TISelectableObject previousSelection;

        /*
        public delegate void ObjectSelectionCallback(TISelectableObject obj);

        public ObjectSelectionCallback OnObjectSelection;
        public ObjectSelectionCallback OnObjectDeselection;
        */

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

                //Let the object(s) know they were selected or deselected.
                previousSelection?.OnUnselected();
                currentSelection?.OnSelected();
            }

        }
    }
}