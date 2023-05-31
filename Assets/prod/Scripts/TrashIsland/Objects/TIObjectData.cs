using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TI Object Settings", menuName = "TrashIsland/Object Settings")]
[System.Serializable]
public class TIObjectData : ScriptableObject
{
    public string objectName;
    public string objectDescription;
    public Sprite objectSprite;
}
