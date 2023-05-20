using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using GAD.Utils;
using TMPro;

namespace GAD375.Prototyper
{
    public class StatusMessages : MonoBehaviour
    {
        public TMP_Text statusMessages;
        // Start is called before the first frame update
        void Start()
        {
            //Register a 'message' function which will print out in the
            //status messages.
            DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
            if (dialogueRunner != null)
            {
                Debug.Log("Installing message function");
                dialogueRunner.AddFunction("message", delegate( string text)
                {
                //    //var text = parameters[0];
                    return Message(text); //.AsString);
                });

                //dialogueRunner.AddCommandHandler<string>("message", (string message) => Message(message) );
            }
        }

        bool Message(string message)
        {
            //Debug.Log("Appending message");
            statusMessages.text += '\n' + message;
            return true;
        }
    }
}