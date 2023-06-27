using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace TrashIsland
{
    public class TIDialogue : TIInteraction
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

        public override void Interact(TIInteractor interactor)
        {
            OnInteractionStart?.Invoke(this, interactor);

            StartDialogue();

            OnInteractionFinish?.Invoke(this, interactor);
        }

    }
}