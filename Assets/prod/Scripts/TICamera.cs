using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
    
    public class TICamera : MonoBehaviour
    {
        public GameObject _target;
        protected Vector3 _defaultCameraOffset;

        // Start is called before the first frame update
        void Start()
        {
            if (_target != null)
            {
                _defaultCameraOffset = transform.position - _target.transform.position;
            }     
        }

        // Update is called once per frame
        void Update()
        {
            if (_target != null)
            {
                transform.position = _target.transform.position + _defaultCameraOffset;
            }
        }
    }
}