using System.Collections;
using System.Collections.Generic;
using TrashIsland;
using UnityEngine;
using Yarn.Unity;

[CreateAssetMenu(fileName = "DialoguePortraits", menuName = "Dialogue Portraits")]
public class DemoDialoguePortraitsStorage : ScriptableObject
{
    public List<DialoguePortraits> portraits = new List<DialoguePortraits>();
}
[System.Serializable]
public class DialoguePortraits
{
    public string charName;
    public Sprite[] emotionSprites;
    public List<string> emotionNames;
}
public class DialoguePortraitPasser : Object
{
    [YarnCommand("SetCharacter")]
    public Sprite PassSprite(string characterName)
    {
        Sprite spriteReturned = null;
        for (int i = 0; i < DemoManager.instance.dialoguePortraits.portraits.Count; i++)
        {
            if (DemoManager.instance.dialoguePortraits.portraits[i].charName == characterName)
            {
                spriteReturned = DemoManager.instance.dialoguePortraits.portraits[i].emotionSprites[0];
            }
            else
            {
                Debug.Log("Name Not Found");
            }
        }
        return spriteReturned;
    }
    [YarnCommand("SetCharacterEmotion")]
    public Sprite PassSprite(string characterName, string emotionName)
    {
        Sprite spriteReturned = null;
        for(int i = 0; i < DemoManager.instance.dialoguePortraits.portraits.Count; i++)
        {
            if (DemoManager.instance.dialoguePortraits.portraits[i].charName == characterName)
            {
                if (DemoManager.instance.dialoguePortraits.portraits[i].emotionNames.Contains(emotionName))
                {
                    int spriteNo = DemoManager.instance.dialoguePortraits.portraits[i].emotionNames.IndexOf(emotionName);
                    spriteReturned = DemoManager.instance.dialoguePortraits.portraits[i].emotionSprites[spriteNo];
                }
                else
                {
                    spriteReturned = DemoManager.instance.dialoguePortraits.portraits[i].emotionSprites[0];
                    Debug.Log("Emotion Name Not Found");
                }
            }
            else
            {
                Debug.Log("Name Not Found");
            }
        }
        
        return spriteReturned;
    }
}