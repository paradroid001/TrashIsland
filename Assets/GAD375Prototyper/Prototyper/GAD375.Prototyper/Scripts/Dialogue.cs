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
                if (r.IsDialogueRunning)
                {
                    //If dialogue is already running, advance it.
                    LineView l = FindObjectOfType<LineView>();
                    if (l != null)
                    {
                        l.OnContinueClicked();
                    }
                }
                else
                {
                    // Kick off the dialogue at this node.
                    r.StartDialogue(node);
                }
            }
        }
    }
}