using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
    [System.Serializable]
    public class FloatRamp
    {
        public float minValue = 0f;
        public float maxValue = 10f;
        public AnimationCurve ramp;

        //parameter to evaluate at.
        public float currentParameter = 0;

        public float currentValue
        {
            get
            {
                return Mathf.Clamp(
                            ((maxValue - minValue) * ramp.Evaluate(currentParameter)),
                            minValue, maxValue);
            }
        }
    }

    public class TICharacterMovement : MonoBehaviour
    {
        protected Vector3 _currentMovement;
        protected Vector3 _currentDestination;
        //how far until a destination is considered 'reached'
        public float movementThreshold = 0.1f;
        public FloatRamp currentSpeed;
        public Animator animator;
        [SerializeField]
        public Rigidbody _rb;

        public Vector3 currentMovement
        {
            get { return _currentMovement; }
        }

        public Vector3 currentDestination
        {
            get { return _currentDestination; }
            set { _currentDestination = value; }
        }

        public Vector3 relativeDestination
        {
            get { return _currentDestination - transform.position; }
            set { _currentDestination = transform.position + value; /*Debug.Log(_currentDestination);*/}
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            _currentDestination = transform.position;
            if (_rb == null)
            {
                _rb = GetComponent<Rigidbody>();
            }
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (!CheckDistance())
            {
                MoveToward();
                RotateTowardCurrentMovement();
            }
            else
            {
                currentSpeed.currentParameter = 0.0f;
            }
            if (animator != null)
            {
                animator.SetFloat("speed", currentSpeed.currentValue);
            }
        }

        protected virtual void RotateTowardCurrentMovement()
        {
            float heading = Mathf.Atan2(_currentMovement.x, _currentMovement.z);
            transform.rotation = Quaternion.EulerAngles(0, heading, 0);
        }

        protected virtual void MoveToward()
        {
            currentSpeed.currentParameter = 1.0f; //todo should be ramped up in value
            _currentMovement = (_currentDestination - transform.position).normalized
                               * currentSpeed.currentValue;

            if (_rb != null) //try rigidbody movement first
            {

                _rb.MovePosition(_rb.position + (_currentMovement * Time.deltaTime));
            }
            else
            {
                transform.position += _currentMovement * Time.deltaTime;
            }
        }

        protected virtual bool CheckDistance()
        {
            if (Vector3.Distance(transform.position, _currentDestination) < movementThreshold)
                return true;
            return false;
        }




    }
}