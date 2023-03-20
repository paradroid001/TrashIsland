using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Seed", menuName = "Inventory/Seed", order = 0)]
public class Seed : InventoryItem 
{
    public enum SeedType{Apple, Carrot, Potato};
    public SeedType seedType;
}
