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
    
}