using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    public enum BinType {Garbage, Recycle, Organic};
    public BinType binType;
    public List<InventoryItem> inBin;
    public Animator anim;
    public bool hasThings;
    public bool sent;
    public bool interact;
    public void Update()
    {
        if(inBin.Capacity > 1)
        {
            hasThings = true;
        }
        if(interact)
        {
            anim.Play("Bin Open");
            anim.SetBool("Open", true);
        }
        else
        {
            anim.SetBool("Open", false);
        }
    }
    public GameObject button;
}
