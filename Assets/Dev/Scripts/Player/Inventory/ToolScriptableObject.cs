using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolAsset", menuName = "Inventory/ToolAsset", order = 0)]
public class ToolScriptableObject : InventoryItem
{
    public enum ToolType{Shovel, Grabber};
}
