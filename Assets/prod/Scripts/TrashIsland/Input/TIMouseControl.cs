using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{
    public class TIMouseControl : TIPlayerInputService
    {
        protected TICharacterMovement _movement;
        public GameObject clickDestinationPrefab;
        private GameObject oldClickDestination = null;

        public override void InitService()
        {
            base.InitService();
        }
        public override void ShutdownService()
        {
            base.ShutdownService();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _movement = GetComponent<TICharacterMovement>();
        }

        public override void CollectInput(float dt)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 dest = GetMousePositionInWorld();
                if (dest != Vector3.zero)
                {
                    _movement.currentDestination = dest;
                    if (oldClickDestination != null)
                    {
                        Destroy(oldClickDestination);
                    }
                    oldClickDestination = Instantiate(clickDestinationPrefab, dest, Quaternion.identity);
                }
            }
        }

        Vector3 GetMousePositionInWorld()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.point;
            }

            return Vector3.zero;
        }
    }
}