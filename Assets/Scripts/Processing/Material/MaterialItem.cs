using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material", menuName = "Inventory/Material", order = 0)]
public class MaterialItem : InventoryItem
{
    public enum MaterialType {plastic, metal, wood, wires};
    public MaterialType material;
    public void OnValidate()
    {
        materialItem = this;
    }
}
