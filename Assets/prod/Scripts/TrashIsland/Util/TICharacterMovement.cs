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
        protected Rigidbody _rb;
        [SerializeField]
        protected TICamera _tiCamera;

        protected float moveTimer; //how long have we been moving?
        protected float cameraDampener;


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
            moveTimer = 0;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (!CheckDistance())
            {
                MoveToward();
                RotateTowardCurrentMovement();
                moveTimer += Time.deltaTime;
                cameraDampener = moveTimer; //camera moves to max after 1 second
            }
            else
            {
                currentSpeed.currentParameter = 0.0f;
                moveTimer = 0;
                cameraDampener = cameraDampener / 2.0f;
                if (cameraDampener < 0.0005f)
                {
                    cameraDampener = 0.0f;
                }
            }
            if (_tiCamera)
            {
                _tiCamera.NotifyMovement(cameraDampener);
            }
            if (animator != null)
            {
                animator.SetFloat("speed", currentSpeed.currentValue);
            }
        }

        protected virtual void RotateTowardCurrentMovement()
        {
            float heading = Mathf.Atan2(_currentMovement.x, _currentMovement.z);
            transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg * heading, 0);
        }

        protected virtual void MoveToward()
        {
            //It turns out it's just too slow to 'ramp up' speed. Just go.
            currentSpeed.currentParameter = 1.0f;
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