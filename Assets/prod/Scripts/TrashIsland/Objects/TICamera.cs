using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{

    public class TICamera : MonoBehaviour
    {
        public GameObject _target;
        protected GameObject _additionalTarget;
        protected Vector3 _defaultCameraOffset;
        //protected Quaternion _defaultCameraRotation;
        public Transform maxExtentTransform; //for zoomout when moving
        public Transform closeupTransform; //for close ups / dialogue
        protected Vector3 _calculatedCameraOffset;
        protected Vector3 _maxExtentOffset;
        protected Vector3 _closeupOffset;

        // Start is called before the first frame update
        void Start()
        {
            if (_target != null)
            {
                _defaultCameraOffset = transform.position - _target.transform.position;
                _closeupOffset = closeupTransform.position - _target.transform.position;
                _maxExtentOffset = maxExtentTransform.position - _target.transform.position;
                _calculatedCameraOffset = _defaultCameraOffset;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_target != null)
            {
                transform.position = _target.transform.position + _calculatedCameraOffset;
            }
        }

        public void NotifyMovement(float movement)
        {
            _calculatedCameraOffset = Vector3.Lerp(_defaultCameraOffset, _maxExtentOffset, Mathf.Clamp(movement, 0, 1));
        }

        public void NotifyDialogue(GameObject other)
        {

        }
    }
}