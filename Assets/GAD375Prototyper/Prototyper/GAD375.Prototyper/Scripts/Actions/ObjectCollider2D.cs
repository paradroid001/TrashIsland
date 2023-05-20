using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GAD.Utils;

namespace GAD375.Prototyper
{
    public class ObjectCollider2D : MonoBehaviour
    {
        public enum CollisionType
        {
            COLLISION,
            TRIGGER
        }

        [System.Serializable]
        public class CollisionAction : UnityEvent<GameObject, GameObject>
        {
            public GameObject arg1;
            public GameObject arg2;
            //public string str;
        }

        [Header("Objects with this tag can collide")]
        [TagSelector]
        public string collisionTag;
        [Header("Which kind of collider is this?")]
        public CollisionType colType = CollisionType.TRIGGER;
        
        public CollisionAction OnEnter = new CollisionAction();
        public CollisionAction OnExit = new CollisionAction();
        public CollisionAction OnStay = new CollisionAction();
        
        bool trigger;

        // Start is called before the first frame update
        void Start()
        {
            trigger = false;
            if (colType == CollisionType.TRIGGER)
                trigger = true;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!trigger || collisionTag == "")
                return;
            if (col.tag == collisionTag)
            {
                OnEnter.Invoke(gameObject, col.gameObject);
            }
        }
        void OnTriggerExit2D(Collider2D col)
        {
            if (!trigger || collisionTag == "")
                return;
            if (col.tag == collisionTag)
            {
                OnExit.Invoke(gameObject, col.gameObject);
            }
        }
        void OnTriggerStay2D(Collider2D col)
        {
            if (!trigger || collisionTag == "")
                return;
            if (col.tag == collisionTag)
            {
                OnStay.Invoke(gameObject, col.gameObject);
            }
        }
        void OnCollisionEnter2D(Collision2D col)
        {
            if (trigger || collisionTag == "")
                return;
            if (col.collider.tag == collisionTag)
            {
                OnEnter.Invoke(gameObject, col.gameObject);
            }
        }
        void OnCollisionExit2D(Collision2D col)
        {
            if (trigger || collisionTag == "")
                return;
            if (col.collider.tag == collisionTag)
            {
                OnExit.Invoke(gameObject, col.gameObject);
            }
        }
        void OnCollisionStay2D(Collision2D col)
        {
            if (trigger || collisionTag == "")
                return;
            if (col.collider.tag == collisionTag)
            {
                OnStay.Invoke(gameObject, col.gameObject);
            }
        }
    }
}