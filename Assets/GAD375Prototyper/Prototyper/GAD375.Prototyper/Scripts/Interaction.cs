using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Yarn.Unity.Example;
using Yarn.Unity;
using GAD375.Prototyper;

namespace GAD375.Prototyper
{
    public class Interaction : MonoBehaviour
    {
        public float interactionRadius = 1.0f;
        protected DialogueRunner dialoguerunner;
        //protected DialogueUI dialogueui;
        protected SimpleCharacterController controller;
        public KeyCode interactKey;

        protected virtual void Awake()
        {
            
        }
        // Start is called before the first frame update
        protected virtual void Start()
        {
            controller = GetComponent<SimpleCharacterController>();
            //Moved getting the dialogue runner out of awake
            //to avoid problems with this component starting and
            //detecting singleton pretenders.
            dialoguerunner = FindObjectOfType<DialogueRunner>();
            
            
            dialoguerunner.onNodeComplete.AddListener(FinishedNode);
            //dialoguerunner.onDialogueStart.AddListener( StartedDialogue);
            dialoguerunner.onNodeStart.AddListener( StartedNode);
            dialoguerunner.onDialogueComplete.AddListener( FinishedDialogue );
        }

        // Update is called once per frame
        protected virtual void Update()
        {
#if ENABLE_LEGACY_INPUT_MANAGER || UNITY_2018
            // Detect if we want to start a conversation
            if (Input.GetKeyDown(interactKey)) 
            {
                TryInteract();
            }
#endif
        }

        public virtual void TryInteract()
        {
            Interactable i = CheckForNearbyInteractable();
            if (i != null)
            {
                i.Interact(gameObject); //interact, passing ourself
            }
        }

        [YarnCommand("respawn")]
        public virtual void Respawn()
        {
            Checkpoint c = GameManager.instance.currentCheckPoint;
            if (c != null)
            {
                c.Respawn(gameObject);
            }
        }

        protected virtual Interactable CheckForNearbyInteractable()
        {
            var allParticipants = new List<Interactable> (FindObjectsOfType<Interactable> ());
            var target = allParticipants.Find (delegate (Interactable p) 
            {
                return p.enabled && // is enabled 
                p.OnInteract.GetPersistentEventCount() > 0 && // has some interaction defined
                ((p.transform.position - this.transform.position)// is in range?
                .magnitude <= (interactionRadius + p.radius));
            });
            return (Interactable)target;
        }

        
        public void FinishedNode(string s)
        {
            Debug.Log("(" + name + ") Finished node: " + s);
        }
        public void FinishedDialogue()//string s)
        {
            Debug.Log("(" + name + ") Finished dialogue "); // + s);
            controller.EnableFPSControls(true);
        }
        public void StartedNode(string s)
        {
            Debug.Log("(" + name + ") Started Node " +s);
            controller.EnableFPSControls(false);
        }
        public void StartedDialogue()
        {
            Debug.Log("(" + name + ") Started Dialogue");
            //controller.EnableFPSControls(false);
        }
        
    }
}
