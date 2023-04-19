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
    public string nameOf;
    public bool hasE;
    GameObject button;
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
            button = GameObject.Instantiate<GameObject>(GameManager.instance.player.interactButtonTemplate);
            button.transform.SetParent(GameManager.instance.player.playerCanvas.transform.GetChild(0));
            button.transform.localPosition = Vector2.zero;
            TextMeshProUGUI buttontext = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttontext.text = nameOf;
            InteractButtons interactButton = button.GetComponent<InteractButtons>();
            interactButton.correspond = gameObject;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.interactable = false;
            player.Interactables.Remove(gameObject);
            GameObject.Destroy(button);
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
            GameManager.instance.inDialogue = true;
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
