using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrashType", menuName = "Inventory/TrashType", order = 0)]
public class TrashType : InventoryItem {
    public GameObject thisObject;
    public string trashName;
    
    public ProductionMachine.Takes typeofMaterial;
    public void OnValidate()
    {
        trashType = this;
    }
}
