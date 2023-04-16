using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Dialogue/Sprites/Sprite Pack")]
public class SpritePack : ScriptableObject
{
    public Sprite[] sprites;
    public Dictionary<string, int> spritePack = new Dictionary<string, int>()
    {
        {"Angry", 0},
        {"Sad", 1},
        {"Default", 2}
    };
}
