using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace GAD375.Prototyper
{

    public class Dialogue : MonoBehaviour
    {
        public string startNode = "";
       
        public virtual void StartDialogue()
        {
            // Kick off the dialogue at this node.
            StartDialogue(startNode);
        }
        public virtual void StartDialogue(string node)
        {
            DialogueRunner r = FindObjectOfType<DialogueRunner>();
            if (r != null)
            {
                {
                    // Kick off the dialogue at this node.
                    r.StartDialogue(node);
                }
            }
        }
    }
}