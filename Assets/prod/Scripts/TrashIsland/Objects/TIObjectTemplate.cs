using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum TIObjectType
{
    PROP,   //generic object, no special functionality
    ITEM,   //Indicates collectable / selectable, not interactable
    TRASH,  //specific item with special properties 
    MACHINE,//interactable
    BUILDING,//interactable
    NPC,    //Generally indicates it can move / speak / interact
    PLAYER  //special case interactable
}

[System.Serializable]
public class TIObjectData
{
    public TIObjectType type;
    public string name;
    public string description;
    public Sprite sprite;

    public TIObjectData(TIObjectType t)
    {
        this.type = t;
    }

    public TIObjectData(TIObjectData from)
    {
        name = from.name;
        description = from.description;
        sprite = from.sprite;
    }
    public void Merge(TIObjectData data)
    {
        type = data.type;
        if (data.name != "")
            name = data.name;
        if (data.description != "")
            description = data.description;
        if (data.sprite != null)
            sprite = data.sprite;
    }
}

[System.Serializable]
public class TISelectableConfigData
{
    public Material selectionMaterial;
    [Range(.002f, 0.2f)]
    public float outlineStrength = 7.0f;
    public float outlineWidth = 7.0f;

    public Color outlineColour = Color.white;
}


[CreateAssetMenu(fileName = "TI Object Data", menuName = "TrashIsland/Object Data")]
[System.Serializable]
public class TIObjectTemplate : ScriptableObject
{
    public TIObjectData objectData;
    public TISelectableConfigData selectableConfig;

    public virtual void Awake()
    {
        if (objectData == null)
            objectData = new TIObjectData(TIObjectType.PROP);
    }
}
