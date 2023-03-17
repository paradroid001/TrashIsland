using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "CraftingRecipe", order = 0)]
public class CraftingRecipe : ScriptableObject
{
    public List<InventoryItem> needed;
    public List<int> amtneeded;
    public InventoryItem makes;
    public Sprite icon;
}
