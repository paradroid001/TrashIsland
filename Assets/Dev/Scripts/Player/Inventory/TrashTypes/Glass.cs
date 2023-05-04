using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Glass", menuName = "Inventory/Trash/Glass")]
public class Glass : TrashType
{
    public bool crushed;
    public bool clean;
    public enum GlassType{Jar, Bottle};
    public GlassType glassType;
}
