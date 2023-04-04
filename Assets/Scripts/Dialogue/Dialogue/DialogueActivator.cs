using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueActivator : MonoBehaviour, Iinteractable
{
    [SerializeField] public DialogueOBJ dialogueOBJ;
    [SerializeField] public DialogueResponseEvents dialogueResponse;
    [SerializeField] private DialogueUI dialogueUI;
    public Sprite charSprite;
    public TextMeshPro charName;
    public bool hasE;
    public void Start()
    {
        dialogueResponse = GetComponent<DialogueResponseEvents>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.interactable = true;
            player.Interactables.Add(gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.interactable = false;
            player.Interactables.Remove(gameObject);
        }
    }
    public void UpdateDialogueObj(DialogueOBJ dialogueOBJ)
    {
        this.dialogueOBJ = dialogueOBJ;
    }
    public void Interact (Player player)
    {
        if(dialogueUI.IsOpen == false)
        {
            foreach(DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
            {
                if(responseEvents.DialogueOBJ == dialogueOBJ)
                {
                    player.dialogueUI.AddResponseEvents(responseEvents.Events);
                    break;
                }
            }
            player.dialogueUI.ShowDialogue(dialogueOBJ);
        }
        
    }
}
