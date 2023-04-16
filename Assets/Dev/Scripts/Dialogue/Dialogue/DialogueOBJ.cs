using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue")]
public class DialogueOBJ : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    public Sprite characterSprite;
    public string charName;
    public string[] Dialogue => dialogue;
    public bool HasResponses =>Responses != null && Responses.Length > 0;
    public Response[] Responses => responses;
    public ResponseEventOBJ dialogueResponse;
    public ResponseEvent events;
}
