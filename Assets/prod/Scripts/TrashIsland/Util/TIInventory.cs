using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
    public class TIInventory : MonoBehaviour, ITIInventory
    {
        public bool AddItem(TIObject item)
        {
            throw new System.NotImplementedException();
        }

        public int GetAvailable()
        {
            throw new System.NotImplementedException();
        }

        public int GetCapacity()
        {
            throw new System.NotImplementedException();
        }

        public TIObject GetOwner()
        {
            throw new System.NotImplementedException();
        }

        public TIObject PeekItemAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public TIObject RemoveItem(TIObject item)
        {
            throw new System.NotImplementedException();
        }

        public int SetCapacity()
        {
            throw new System.NotImplementedException();
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