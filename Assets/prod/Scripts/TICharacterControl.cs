using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
    public class TICharacterControl : MonoBehaviour
    {
        protected TICharacterMovement _movement;

        // Start is called before the first frame update
        void Start()
        {
            _movement = GetComponent<TICharacterMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_movement != null)
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                if (v != 0 || h != 0)
                {
                    _movement.relativeDestination = transform.forward * v + transform.right * h; // + transform.up * transform.position.y;
                }
            }
        }
    }
}