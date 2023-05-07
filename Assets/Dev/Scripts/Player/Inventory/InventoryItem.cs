using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Inventory/InventoryItem", order = 0)]
public class InventoryItem : ScriptableObject 
{
    public Seed seed;
    public MaterialItem materialItem;
    public InventoryButtons button;
    public TrashType trashType;
    public ToolScriptableObject toolsType;
    public BuildingAsset buildType;
    public Sprite inventorySprite;
    public enum IsTrash{Trash, NotTrash}; 
    public IsTrash isTrash;
    public enum TypeOfTrash{Garbage, Recycle, Organic};
    public TypeOfTrash typeOfTrash;
    public GameObject objectOfTrash;
}
