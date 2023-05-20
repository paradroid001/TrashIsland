using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GAD375.Prototyper
{
    public class Interactable : MonoBehaviour
    {
        public string interactableName = "Unknown";
        [System.Serializable]
        public class Interactions : UnityEvent<GameObject>
        {
            public GameObject gameObject;
        }
        public float radius = 1f;

        public Interactions OnInteract = new Interactions();

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public void Interact(GameObject g)
        {
            OnInteract.Invoke(g);
        }
    }
}