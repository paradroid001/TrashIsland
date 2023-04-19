using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int recyclePoints;
    public Player player;
    public GameObject inventory;
    public Inventory invent;
    public List<Trash> trash;
    public List<Recycle> recycles;
    public List<Garbage> garbages;
    public GameObject craftingMenu;
    public List<InventoryItem> trashTypesinBin;
    public List<int> trashTypesinBinAmt;
    public List<MaterialItem> materials;
    public RoboInventory roboInventory;
    public GameObject floor;
    public ToolScriptableObject shovel;
    public ToolScriptableObject pick;
    public BuildingAsset build;
    public bool inDialogue;
    void Start()
    {
        instance = this;
        Trash[] trashes = FindObjectsOfType<Trash>();
        trash = new List<Trash>(trashes);
        Recycle[] recycle = FindObjectsOfType<Recycle>();
        recycles = new List<Recycle>(recycle);
        Garbage[] garbage = FindObjectsOfType<Garbage>();
        garbages = new List<Garbage>(garbage);
        invent.AddToInventory(shovel);
        invent.AddToInventory(pick);
        invent.AddToInventory(build);
    }
    public void RemoveFromTrashIvent(InventoryItem item)
    {
        //take the ingredient needed out for crafting
        if(trashTypesinBin.Contains(item))
        {
            int indOfItem = trashTypesinBin.IndexOf(item);
            if(trashTypesinBinAmt[indOfItem] > 0)
            {
                trashTypesinBinAmt[indOfItem]--;
                if(trashTypesinBinAmt[indOfItem] <= 0)
                {
                    trashTypesinBin.Remove(trashTypesinBin[indOfItem]);
                    trashTypesinBinAmt.Remove(trashTypesinBinAmt[indOfItem]);
                }
            }
        }
    }
}
