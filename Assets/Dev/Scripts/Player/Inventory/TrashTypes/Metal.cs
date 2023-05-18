using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Metal", menuName = "Inventory/Trash/Metal", order = 0)]
public class Metal : TrashType
{
    public bool clean;
    public bool shredded;
    public bool crushed;
    public bool melted;
    public enum MetalType{Can, Lid, Tin};
    public MetalType metalType;
}
