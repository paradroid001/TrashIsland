using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plastic", menuName = "Inventory/Trash/Plastic", order = 0)]
public class Plastic : TrashType
{
    public bool clean;
    public bool shredded;
    public enum PlasticType {Lid, Container, Bottle};
    public PlasticType plasticType;
}
