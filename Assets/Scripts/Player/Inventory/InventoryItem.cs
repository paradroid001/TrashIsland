using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Inventory/InventoryItem", order = 0)]
public class InventoryItem : ScriptableObject 
{
    public Sprite inventorySprite;
    public enum IsTrash{Trash, NotTrash};
    public IsTrash isTrash;
    public enum TypeOfTrash{Garbage, Recycle};
    public TypeOfTrash typeOfTrash;
}
