using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueOBJ dialogueOBJ;

    public string ResponseText => responseText;

    public DialogueOBJ DialogueOBJ => dialogueOBJ;
}
