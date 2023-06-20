using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingList : MonoBehaviour
{
    public static CraftingList instance;
    public int whichOne;
    public List<CraftingRecipe> manuals;
    public void Start()
    {
        instance = this;
    }
}
