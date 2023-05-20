using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD375.Prototyper
{
    [RequireComponent(typeof(Rigidbody))]
    
    public class SimpleCharacterController : MonoBehaviour
    {
        public Camera fpCamera;
        public float moveSpeed = 1.0f;
        public Vector2 mouseSensitivity;
        public bool invertVertical = false;
        bool controlsEnabled = true;

        Rigidbody rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationZ |
                             RigidbodyConstraints.FreezeRotationY |
                            RigidbodyConstraints.FreezeRotationX;
        }

        // Start is called before the first frame update
        void Start()
        {
            LockCursor();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        //We collect inputs (with the old input system) during fixed update
        //since we're going to be doing physics based movement calculations,
        //and it's easier to not have to accumulate multi frame input in update.
        //we scale all inputs before passing them to Perform Movement.
        void FixedUpdate()
        {
            if (controlsEnabled)
            {
                Vector2 moveinput = new Vector2( Input.GetAxis("Horizontal") * moveSpeed, 
                                                Input.GetAxis("Vertical") * moveSpeed ); 
                                                
                Vector2 mousedelta = new Vector2( Input.GetAxis("Mouse X") * mouseSensitivity.x,
                                                Input.GetAxis("Mouse Y") * mouseSensitivity.y);
                                                
                PerformMovement(moveinput * Time.fixedDeltaTime, mousedelta * Time.fixedDeltaTime);
            }    
        }

        void PerformMovement(Vector2 move, Vector2 turn)
        {
            float xrot = turn.y;
            if (invertVertical)
                xrot = -xrot;
            fpCamera.transform.Rotate(new Vector3(xrot, 0, 0));
            Quaternion yrot = Quaternion.Euler( new Vector3(0, 
                                            turn.x, 
                                            0) );
            rb.MoveRotation(rb.rotation * yrot);

            Vector3 movement = transform.forward * move.y + 
                            transform.right * move.x;
            rb.MovePosition(rb.position + movement);
        }

        void LockCursor(bool shouldLock = true)
        {
            if (shouldLock)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public void EnableFPSControls(bool shouldEnable)
        {
            controlsEnabled = shouldEnable;
            LockCursor(shouldEnable);
        }
    }
}