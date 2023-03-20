using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    public enum BinType {Garbage, Recycle, Organic};
    public BinType binType;
    public List<InventoryItem> inBin;
}
