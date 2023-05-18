using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    //[SerializeField] private DialogueOBJ testDialogue;

    public bool IsOpen { get; private set; }
    private TypeWriterEffect typeWriterEffect;
    private ResponseHandler responseHandler;
    public Image charSprite;
    public TextMeshProUGUI charName;
    public Canvas canvas;
    public GameObject speaker;
    public void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        canvas = transform.GetComponent<Canvas>();
        //ShowDialogue(testDialogue);
        //CloseDialogueBox();
    }
    public void ShowDialogue(DialogueOBJ dialogueOBJ)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        if (dialogueOBJ.characterSprite != null)
        {
            charSprite.sprite = dialogueOBJ.characterSprite;
            charSprite.color = Color.white;
        }
        if (!string.IsNullOrWhiteSpace(dialogueOBJ.charName))
        {
            charName.transform.parent.gameObject.SetActive(true);
            charName.text = dialogueOBJ.charName;
        }
        else if(string.IsNullOrWhiteSpace(dialogueOBJ.charName))
        {
            charName.transform.parent.gameObject.SetActive(false);
        }
        StartCoroutine(StepThroughDialogue(dialogueOBJ));
    }
    private IEnumerator StepThroughDialogue(DialogueOBJ dialogueOBJ)
    {
        if(IsOpen)
        {
            //yield return new WaitForSeconds(2);
        

            for(int i = 0; i < dialogueOBJ.Dialogue.Length; i++)
            {
                string dialogue = dialogueOBJ.Dialogue[i];
                yield return RunTypingEffect(dialogue);
                //Debug.Log(i);
                textLabel.text = dialogue;
                if(speaker.GetComponent<DialogueEvents>() != null)
                {
                    DialogueEvents speakA = speaker.GetComponent<DialogueEvents>();
                    if(speakA.diaEvents[i].align[i] == i)
                    {
                        Debug.Log(i);
                        dialogueOBJ.diaEvents[i].DiaEvent.Invoke();
                    }
                }
                
                if (i == dialogueOBJ.Dialogue.Length - 1 && dialogueOBJ.HasResponses) break;
                yield return null;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
            }
            
            
            if(dialogueOBJ.HasResponses)
            {
                responseHandler.ShowResponses(dialogueOBJ.Responses);
            }
            else
            {
                CloseDialogueBox();
            }
            //CloseDialogueBox();
        }
        
    }
    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }
    public void CloseDialogueBox()
    {
        GameManager.instance.inDialogue = false;
        canvas.sortingOrder = 0;
        IsOpen = false;
        dialogueBox.SetActive(false);
        charSprite.sprite = null;
        charSprite.color = Color.clear;
        charName.transform.parent.gameObject.SetActive(true);
        charName.text = null;
        textLabel.text = string.Empty;
        
    }
    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriterEffect.Run(dialogue, textLabel);
        while(typeWriterEffect.isRunning)
        {
            yield return null;

            if(Input.GetKeyDown(KeyCode.E))
            {
                typeWriterEffect.Stop();
            }
        }
    }
}
