using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/ResponseEvent")]
public class ResponseEventOBJ : ScriptableObject
{
    [SerializeField] private DialogueOBJ dialogueOBJ;
    [SerializeField] private ResponseEvent[] events;
    public DialogueOBJ DialogueOBJ => dialogueOBJ;

    public ResponseEvent[] Events => events;

    public void OnValidate()
    {
        if (dialogueOBJ == null) return;
        if (dialogueOBJ.Responses == null) return;
        if (events != null && events.Length == dialogueOBJ.Responses.Length) return;

        if (events == null)
        {
            events = new ResponseEvent[dialogueOBJ.Responses.Length];
        }
        else
        {
            Array.Resize(ref events, dialogueOBJ.Responses.Length);
        }
        for (int i = 0; i < dialogueOBJ.Responses.Length; i++)
        {
            Response response = dialogueOBJ.Responses[i];
            if (events[i] != null)
            {
                events[i].name = response.ResponseText;
            }
            events[i] = new ResponseEvent() { name = response.ResponseText };
        }
    }
}
