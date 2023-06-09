using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{

    public class TICollect : TIInteraction
    {
        //Try to collect the item.
        //The interactor has to have an inventory
        public override void Interact(TIInteractor interactor)
        {
            OnInteractionStart?.Invoke(this, interactor);

            TIInventory destInventory = interactor.GetComponent<TIInventory>();
            TIObject thisObject = GetComponent<TIObject>();
            if (destInventory != null && thisObject != null)
            {
                if (!destInventory.AddItem(thisObject))
                {
                    Debug.LogError("Coudn't add");
                }
            }
            else
            {
                Debug.LogError("No object, or no dest inventory");
            }
            OnInteractionFinish?.Invoke(this, interactor);
        }
    }
}