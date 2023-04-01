using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
    public class TIMouseControl : MonoBehaviour
    {
        protected TICharacterMovement _movement;
        public GameObject clickDestinationPrefab;
        private GameObject oldClickDestination = null;
        // Start is called before the first frame update
        void Start()
        {
            _movement = GetComponent<TICharacterMovement>();
        }

        // Update is called once per frame
        void Update()
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