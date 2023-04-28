using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building Asset", menuName = "Inventory/BuildingAsset")]
public class BuildingAsset : InventoryItem
{
    public float x;
    public float y;
    public GameObject building;
}
