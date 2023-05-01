using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int capacity;
    public enum InventoryReason{Look, Bin, Process};
    public InventoryReason reason;
    public GameObject button;
    public GameObject processButton;
    public GameObject backButton;
    public List<Image> images;
    public int[] amount; 
    public List<InventoryItem> inventoryItems;
    public List<InventoryButtons> selected;
    public Bin currentBin;
    public ProductionMachine currentMachine;
    public GameObject dropButton;
    public Image equipImg;
    public void Start()
    {
        //Get the buttons to click on.
        button = transform.GetChild(0).GetChild(2).gameObject;
        processButton = transform.GetChild(0).GetChild(3).gameObject;
        backButton = transform.GetChild(0).GetChild(1).gameObject;
        List<InventoryButtons> inventoryButtons = new List<InventoryButtons>(12);
        for(int i = 0; i <= transform.GetChild(0).GetChild(0).childCount - 1; i++)
        {
            inventoryButtons.Add(transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<InventoryButtons>());
            inventoryButtons[i].inventory = this;
        }
    }
    public void OnValidate() 
    {
        //Maintain the ratio of inventory slots to the amount list
        if(images.Count == 0)
        {
            for(int i = 0; i < transform.GetChild(0).GetChild(0).childCount; i++)
            {
                images.Add(transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<Image>());
            }
            inventoryItems.Capacity = images.Count;
        }
    }
    
    public void Return(GameObject Inventory)
    {
        //Get out of inventory menu
        reason = InventoryReason.Look;
        button.SetActive(false);
        Inventory.SetActive(false);
        Time.timeScale = 1;
        
    }
    public void Update()
    {
        switch(reason)
        {
            case InventoryReason.Look:
                button.SetActive(false);
                processButton.SetActive(false);
                backButton.SetActive(false);
                //Time.timeScale = 1;
                break;
            case InventoryReason.Bin:
                button.SetActive(true);
                backButton.SetActive(true);
                Time.timeScale = 0;
                break;
            case InventoryReason.Process:
                processButton.SetActive(true);
                backButton.SetActive(true);
                Time.timeScale = 0;
                break;
        }
    }
    public void AddToInventory(InventoryItem trashtype)
    {
        if(inventoryItems.Count <= capacity)
        {
            if(!inventoryItems.Contains(trashtype))
            {
                inventoryItems.Add(trashtype);
                for(int i = 0; i <= images.Count; i++)
                {
                    if(trashtype.inventorySprite != null)
                    {
                        if(images[i].sprite == null)
                        {
                            //if the item is not already in the inventory, and it has an assigned sprite, then present the sprite and let it know the data of the thing in the slot
                            images[i].sprite = trashtype.inventorySprite;
                            images[i].GetComponent<InventoryButtons>().item = trashtype;
                            images[i].GetComponent<InventoryButtons>().selectable = true;
                            images[i].color = Color.white;
                            break;
                        }
                    }
                    else return;
                }
            }
            else if(inventoryItems.Contains(trashtype))
            {
                //if the item is already in the inventory, then add to the amt
                Debug.Log("Add amount");
                for(int i = 0; i < inventoryItems.Count; i++)
                {
                    if(inventoryItems[i] == trashtype)
                    {
                        amount[i]++;
                        break;
                    }
                }
            }
        }
        else 
        {
            AddToRoboInventory(trashtype);
        }
    }
    public void AddToRoboInventory(InventoryItem trashtype)
    {
        if(RoboInventory.roboInventory.inventoryItems.Capacity < RoboInventory.roboInventory.capacity)
            {
                if(!RoboInventory.roboInventory.inventoryItems.Contains(trashtype))
                {
                    RoboInventory.roboInventory.inventoryItems.Add(trashtype);
                        if(trashtype.inventorySprite != null)
                        {
                            for(int i = 0; i < RoboInventory.roboInventory.images.Capacity; i++)
                            {
                                if(RoboInventory.roboInventory.roboInventoryButtons[i].used == false)
                                {
                                     Debug.Log("W");
                                    //if the item is not already in the inventory, and it has an assigned sprite, then present the sprite and let it know the data of the thing in the slot
                                    RoboInventory.roboInventory.images[i].sprite = trashtype.inventorySprite;
                                    RoboInventory.roboInventory.images[i].GetComponent<RoboIntventoryButtons>().item = trashtype;
                                    RoboInventory.roboInventory.images[i].GetComponent<RoboIntventoryButtons>().selectable = true;
                                    RoboInventory.roboInventory.images[i].color = Color.white;
                                    RoboInventory.roboInventory.roboInventoryButtons[i].used = true;
                                    break;
                                }
                            }
                        }
                        else return;
                }
                else
                {
                    int amtIndex = RoboInventory.roboInventory.inventoryItems.IndexOf(trashtype);
                    RoboInventory.roboInventory.amount[amtIndex]++;
                }
            }
    }
   
    public void PutInBin(Bin bin)
    {
        //Debug.Log("B");
        for(int i = 0; i < selected.Count; i++)
        {
            PutInBinB(i, bin);
        }
        selected.Clear();
    }
    public void PutInBinA()
    {
        //this goes in an event
        PutInBin(currentBin);
        //selected.Clear();
    } 
    public void PutInBinB(int i, Bin bin)
    {
        if(selected[i].item.isTrash == InventoryItem.IsTrash.Trash)
        {
            if(selected[i].item.typeOfTrash == InventoryItem.TypeOfTrash.Recycle)
            {
                if(bin.binType == Bin.BinType.Recycle)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        int ind = inventoryItems.IndexOf(selected[i].item);
                        if(amount[ind] >= 1)
                        {
                            bin.inBin.Add(selected[i].item);
                            amount[ind]--;
                        }
                        else if(amount[ind] == 0)
                        {
                            bin.inBin.Add(selected[i].item);
                            selected[i].item = null;
                            selected[i].GetComponent<Image>().sprite = null;
                            selected[i].GetComponent<Image>().color = Color.red;
                            inventoryItems.Remove(selected[i].item);
                            selected[i].item = null;
                        }
                        Debug.Log(ind);
                    }
                    GameManager.instance.recyclePoints += 10;
                }
                else if(bin.binType == Bin.BinType.Garbage)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        int ind = inventoryItems.IndexOf(selected[i].item);
                        if(amount[ind] >= 1)
                        {
                            bin.inBin.Add(selected[i].item);
                            amount[ind]--;
                        }
                        else if(amount[ind] == 0)
                        {
                            bin.inBin.Add(selected[i].item);
                            selected[i].item = null;
                            selected[i].GetComponent<Image>().sprite = null;
                            selected[i].GetComponent<Image>().color = Color.red;
                            inventoryItems.Remove(selected[i].item);
                        }
                        Debug.Log(ind);
                    }
                }   
                else if(bin.binType == Bin.BinType.Organic)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        int ind = inventoryItems.IndexOf(selected[i].item);
                        if(amount[ind] >= 1)
                        {
                            bin.inBin.Add(selected[i].item);
                            amount[ind]--;
                        }
                        else if(amount[ind] == 0)
                        {
                            bin.inBin.Add(selected[i].item);
                            selected[i].item = null;
                            selected[i].GetComponent<Image>().sprite = null;
                            selected[i].GetComponent<Image>().color = Color.red;
                            inventoryItems.Remove(selected[i].item);
                        }
                        Debug.Log(ind);
                    }
                    GameManager.instance.recyclePoints -= 10;
                }
            }
            else if(selected[i].item.typeOfTrash == InventoryItem.TypeOfTrash.Garbage)
            {
                if(bin.binType == Bin.BinType.Garbage)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        int ind = inventoryItems.IndexOf(selected[i].item);
                        if(amount[ind] >= 1)
                        {
                            bin.inBin.Add(selected[i].item);
                            amount[ind]--;
                        }
                        else if(amount[ind] == 0)
                        {
                            bin.inBin.Add(selected[i].item);
                            selected[i].item = null;
                            selected[i].GetComponent<Image>().sprite = null;
                            selected[i].GetComponent<Image>().color = Color.red;
                            inventoryItems.Remove(selected[i].item);
                        }
                        Debug.Log(ind);
                    }
                    GameManager.instance.recyclePoints += 10;
                }
                else if(bin.binType == Bin.BinType.Organic)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        int ind = inventoryItems.IndexOf(selected[i].item);
                        if(amount[ind] >= 1)
                        {
                            bin.inBin.Add(selected[i].item);
                            amount[ind]--;
                        }
                        else if(amount[ind] == 0)
                        {
                            bin.inBin.Add(selected[i].item);
                            selected[i].item = null;
                            selected[i].GetComponent<Image>().sprite = null;
                            selected[i].GetComponent<Image>().color = Color.red;
                            inventoryItems.Remove(selected[i].item);
                        }
                        Debug.Log(ind);
                    }
                    GameManager.instance.recyclePoints -= 10;
                }
                else if(bin.binType == Bin.BinType.Recycle)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        int ind = inventoryItems.IndexOf(selected[i].item);
                        if(amount[ind] >= 1)
                        {
                            bin.inBin.Add(selected[i].item);
                            amount[ind]--;
                        }
                        else if(amount[ind] == 0)
                        {
                            bin.inBin.Add(selected[i].item);
                            selected[i].item = null;
                            selected[i].GetComponent<Image>().sprite = null;
                            selected[i].GetComponent<Image>().color = Color.red;
                            inventoryItems.Remove(selected[i].item);
                        }
                        Debug.Log(ind);
                    }
                    GameManager.instance.recyclePoints -= 10;
                }
                else if(bin.binType == Bin.BinType.Organic)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        int ind = inventoryItems.IndexOf(selected[i].item);
                        if(amount[ind] >= 1)
                        {
                            bin.inBin.Add(selected[i].item);
                            amount[ind]--;
                        }
                        else if(amount[ind] == 0)
                        {
                            bin.inBin.Add(selected[i].item);
                            selected[i].item = null;
                            selected[i].GetComponent<Image>().sprite = null;
                            selected[i].GetComponent<Image>().color = Color.red;
                            inventoryItems.Remove(selected[i].item);
                        }
                        Debug.Log(ind);
                    }
                    GameManager.instance.recyclePoints -= 10;
                }
            }
            else if(selected[i].item.typeOfTrash == InventoryItem.TypeOfTrash.Organic)
            {
                if(bin.binType == Bin.BinType.Garbage)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        int ind = inventoryItems.IndexOf(selected[i].item);
                        if(amount[ind] >= 1)
                        {
                            bin.inBin.Add(selected[i].item);
                            amount[ind]--;
                        }
                        else if(amount[ind] == 0)
                        {
                            bin.inBin.Add(selected[i].item);
                            selected[i].item = null;
                            selected[i].GetComponent<Image>().sprite = null;
                            selected[i].GetComponent<Image>().color = Color.red;
                            inventoryItems.Remove(selected[i].item);
                        }
                        Debug.Log(ind);
                    }
                    GameManager.instance.recyclePoints += 10;
                }
                else if(bin.binType == Bin.BinType.Organic)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        int ind = inventoryItems.IndexOf(selected[i].item);
                        if(amount[ind] >= 1)
                        {
                            bin.inBin.Add(selected[i].item);
                            amount[ind]--;
                        }
                        else if(amount[ind] == 0)
                        {
                            bin.inBin.Add(selected[i].item);
                            selected[i].item = null;
                            selected[i].GetComponent<Image>().sprite = null;
                            selected[i].GetComponent<Image>().color = Color.red;
                            inventoryItems.Remove(selected[i].item);
                        }
                        Debug.Log(ind);
                    }
                    GameManager.instance.recyclePoints -= 10;
                }
                else if(bin.binType == Bin.BinType.Organic)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        int ind = inventoryItems.IndexOf(selected[i].item);
                        if(amount[ind] >= 1)
                        {
                            bin.inBin.Add(selected[i].item);
                            amount[ind]--;
                        }
                        else if(amount[ind] == 0)
                        {
                            bin.inBin.Add(selected[i].item);
                            selected[i].item = null;
                            selected[i].GetComponent<Image>().sprite = null;
                            selected[i].GetComponent<Image>().color = Color.red;
                            inventoryItems.Remove(selected[i].item);
                        }
                        Debug.Log(ind);
                    }
                    GameManager.instance.recyclePoints -= 10;
                }
                else if(bin.binType == Bin.BinType.Organic)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        int ind = inventoryItems.IndexOf(selected[i].item);
                        if(amount[ind] >= 1)
                        {
                            bin.inBin.Add(selected[i].item);
                            amount[ind]--;
                        }
                        else if(amount[ind] == 0)
                        {
                            bin.inBin.Add(selected[i].item);
                            selected[i].item = null;
                            selected[i].GetComponent<Image>().sprite = null;
                            selected[i].GetComponent<Image>().color = Color.red;
                            inventoryItems.Remove(selected[i].item);
                        }
                        Debug.Log(ind);
                    }
                    GameManager.instance.recyclePoints += 10;
                }
            }
        }
        else if(selected[i].item.isTrash == InventoryItem.IsTrash.NotTrash)
        {

        }
        //bin.inBin.Add(selected[i].item);
    }
    public void PutInProcessor()
    {
        for(int i = 0; i < selected.Count; i++)
        {
            if(selected[i].item.trashType != null)
            {
                if(currentMachine.contains.Capacity == 0 )
                {
                    Debug.Log("IUB");
                    if(currentMachine.takes.HasFlag(selected[i].item.trashType.typeofMaterial))
                    {
                        if(amount[i] == 0)
                        {
                            Debug.Log("Process");
                            inventoryItems.Remove(selected[i].item);
                            currentMachine.PutIn(selected[i].item);
                            selected[i].item = null;
                            selected[i].GetComponent<Image>().sprite = null;
                            selected[i].GetComponent<Image>().color = Color.red;
                            
                        }
                        else if(amount[i] >= 1)
                        {
                            currentMachine.PutIn(selected[i].item);
                            amount[i]--;
                        }
                    }
                }
                else if(currentMachine.contains.Capacity >= 1)
                {
                    if(selected[i].item.trashType == currentMachine.contains[0])
                    {
                        if(currentMachine.takes.HasFlag(selected[i].item.trashType.typeofMaterial))
                        {
                            if(amount[i] == 0)
                            {
                                Debug.Log("Process");
                                inventoryItems.Remove(selected[i].item);
                                currentMachine.PutIn(selected[i].item);
                                selected[i].item = null;
                                selected[i].GetComponent<Image>().sprite = null;
                                selected[i].GetComponent<Image>().color = Color.red;
                                
                            }
                            else if(amount[i] >= 1)
                            {
                                currentMachine.PutIn(selected[i].item);
                                amount[i]--;
                            }
                        }
                    }
                }
            }
        }
    }
    public void DropItem()
    {
        GameObject spawned = GameObject.Instantiate(selected[0].item.objectOfTrash);
        spawned.transform.parent = GameManager.instance.player.transform;
        spawned.transform.localPosition = Vector3.forward;
        spawned.transform.parent = null;
        if(inventoryItems.Contains(selected[0].item))
        {
            int amtIndex = inventoryItems.IndexOf(selected[0].item);
            if(amount[amtIndex] >= 1)
            {
                amount[amtIndex]--;
            }
            else if(amount[amtIndex] == 0)
            {
                inventoryItems.Remove(selected[0].item);
                selected[0].item = null;
                selected[0].GetComponent<Image>().sprite = null;
                selected[0].GetComponent<Image>().color = Color.red;
            }
        }
        
    }
    public void EquipTool()
    {
        if(selected[0].item.toolsType != null)
        {
            GameManager.instance.player.equippedTool = selected[0].item.toolsType;
            equipImg.sprite = selected[0].item.inventorySprite;
            GameManager.instance.player.hold.GetChild(selected[0].item.toolsType.indexAsChild).gameObject.SetActive(true);
        }
    }
    public void PlaceBuilding()
    {
        GridDetector detector = GameManager.instance.player.detector;
        if(detector.inFront.Count <= 1)
        {
            if(selected[0].item.buildType)
            {
                GridSquare squareInFront = detector.inFront[0];
                GameObject built = GameObject.Instantiate<GameObject>(selected[0].item.buildType.building);
                built.transform.SetParent(detector.inFront[0].transform);
                built.transform.localPosition = Vector3.zero;
                squareInFront.built = true;
                inventoryItems.Remove(selected[0].item);
                selected[0].item = null;
                selected[0].GetComponent<Image>().sprite = null;
                selected[0].GetComponent<Image>().color = Color.red;
            }
        }
    }
    public void CloseBin(Bin bin)
    {
        bin.anim.SetBool("Open", false);
    }
    public void BinFollow()
    {
        currentBin.following = true;
    }
}
