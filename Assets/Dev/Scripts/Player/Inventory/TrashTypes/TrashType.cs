using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrashType", menuName = "Inventory/Trash/TrashType", order = 0)]
public class TrashType : InventoryItem {
    public int hpCost;
    public GameObject thisObject;
    public string trashName;
    public Plastic plastic;
    public Metal metal;
    
    public ProductionMachine.Takes typeofMaterial;
    public void OnValidate()
    {
        trashType = this;
    }
}
