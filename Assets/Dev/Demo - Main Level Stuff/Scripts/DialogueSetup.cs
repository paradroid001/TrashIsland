using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueSetup : MonoBehaviour
{
    [SerializeField]
    string activeNode;
    void Start()
    {
        DialogueRunner r = FindObjectOfType<DialogueRunner>();
            if (r != null)
            {
                if (r.IsDialogueRunning)
                {
                    Debug.Log("Error: Dialogue Already Running");
                }
                else
                {
                    // Kick off the dialogue at this node.

                    r.StartDialogue(activeNode);
                }
                Destroy(this);
            }
    }
}
