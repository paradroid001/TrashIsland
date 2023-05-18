using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int capacity;
    public enum InventoryReason{Look, Bin, Process};
    public InventoryReason reason;
    public RoboInventory roboInventory;
    public bool organise;
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
    public GameObject nameDisplay;
    public List<InvButtons> swap;
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
    public void Organise()
    {
        if(organise == false)
        {
            organise = true;
            roboInventory.organise = true;
        }
        else if(organise)
        {
            organise = false;
            roboInventory.organise = false;
        }
    }
    public void MoveStuff()
    {
        Debug.Log("AAAAA");
        InvButtons button1 = swap[0];
        InventoryItem item1 = null;
        int ind1 = 0;
        int amt1 = 0;
        int ind2 = 0;
        int amt2 = 0;
        if(button1.roboIntventoryButtons != null)
        {
            item1 = button1.roboIntventoryButtons.item;
            ind1 = roboInventory.inventoryItems.IndexOf(item1);
            amt1 = roboInventory.amount[ind1];
        }
        else if (button1.inventoryButtons != null)
        {
            item1 = button1.inventoryButtons.item;
            ind1 = inventoryItems.IndexOf(item1);
            amt1 = amount[ind1];
        }
        InvButtons button2 = swap[1];
        InventoryItem item2 = null;
        if(button2.roboIntventoryButtons != null)
        {
            item2 = button2.roboIntventoryButtons.item;
            ind2 = roboInventory.inventoryItems.IndexOf(item2);
            amt2 = roboInventory.amount[ind2];
        }
        else if (button2.inventoryButtons != null)
        {
            item2 = button2.inventoryButtons.item;
            ind2 = inventoryItems.IndexOf(item2);
            amt2 = amount[ind2];
        }

        //do the swap

        if(button1.roboIntventoryButtons != null && button2.roboIntventoryButtons)
        {
            button1.roboIntventoryButtons.item = item2;
            roboInventory.amount[ind1] = amt2;
            roboInventory.inventoryItems[ind1] = item2;
            button2.roboIntventoryButtons.item = item1;
            roboInventory.amount[ind2] = amt1;
            roboInventory.inventoryItems[ind2] = item1;
            button1.GetComponent<Image>().sprite = item2.inventorySprite; 
            button1.roboIntventoryButtons.number.text = amt2.ToString();
            button2.GetComponent<Image>().sprite = item1.inventorySprite;
            button2.roboIntventoryButtons.number.text = amt1.ToString();

        }
        else if(button1.roboIntventoryButtons != null && button2.inventoryButtons != null)
        {
            button1.roboIntventoryButtons.item = item2;
            roboInventory.amount[ind1] = amt2;
            roboInventory.inventoryItems[ind1] = item2;
            button2.inventoryButtons.item = item1;
            inventoryItems[ind2] = item1;
            amount[ind2] = amt1;
            button1.GetComponent<Image>().sprite = item2.inventorySprite;
            button1.roboIntventoryButtons.number.text = amt2.ToString();
            button2.GetComponent<Image>().sprite = item1.inventorySprite;
            button2.inventoryButtons.number.text = amt1.ToString();
        }
        else if(button1.inventoryButtons != null && button2.roboIntventoryButtons != null)
        {
            button1.inventoryButtons.item = item2;
            amount[ind1] = amt2;
            inventoryItems[ind1] = item2;
            button2.roboIntventoryButtons.item = item1;
            roboInventory.amount[ind2] = amt1;
            roboInventory.inventoryItems[ind2] = item1;
            button1.GetComponent<Image>().sprite = item2.inventorySprite;
            button1.inventoryButtons.number.text = amt2.ToString();
            button2.GetComponent<Image>().sprite = item1.inventorySprite;
            button2.roboIntventoryButtons.number.text = amt1.ToString();
        }
        else if(button1.inventoryButtons != null && button2.inventoryButtons != null)
        {
            button1.inventoryButtons.item = item2;
            amount[ind1] = amt2;
            inventoryItems[ind1] = item2;
            button2.inventoryButtons.item = item1;
            amount[ind2] = amt1;
            inventoryItems[ind2] = item1;
            button1.GetComponent<Image>().sprite = item2.inventorySprite;
            button1.inventoryButtons.number.text = amt2.ToString();
            button2.GetComponent<Image>().sprite = item1.inventorySprite;
            button2.inventoryButtons.number.text = amt1.ToString();
        }
        swap.Clear();
    }
    public void Return(GameObject Inventory)
    {
        //Get out of inventory menu
        reason = InventoryReason.Look;
        organise = false;
        roboInventory.organise = false;
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
        if(!inventoryItems.Contains(trashtype) )
        {
            if(inventoryItems.Count < capacity)
            {
                inventoryItems.Add(trashtype);
                for(int i = 0; i <= images.Count; i++)
                {
                    if(trashtype.inventorySprite != null)
                    {
                        if(images[i].sprite == null)
                        {
                            //if the item is not already in the inventory, and it has an assigned sprite, then present the sprite and let it know the data of the thing in the slot
                            
                            int amtIndex = inventoryItems.IndexOf(trashtype);
                            trashtype.button = images[amtIndex].GetComponent<InventoryButtons>();
                            amount[amtIndex]++;
                            images[i].sprite = trashtype.inventorySprite;
                            images[i].GetComponent<InventoryButtons>().item = trashtype;
                            images[i].GetComponent<InventoryButtons>().selectable = true;
                            images[i].color = Color.white;
                            images[i].GetComponent<InventoryButtons>().number.text = amount[amtIndex].ToString();
                            break;
                        }
                    }
                    else return;
                }
            }
            else if(inventoryItems.Count >= capacity)
            {/*
                //if the item is already in the inventory, then add to the amt
                Debug.Log("Add amount");
                for(int i = 0; i < inventoryItems.Count; i++)
                {
                    if(inventoryItems[i] == trashtype)
                    {
                        amount[i]++;
                        images[i].GetComponent<InventoryButtons>().number.text = amount[i].ToString();
                        break;
                    }
                }*/
                AddToRoboInventory(trashtype);
            }
        }
        else if(inventoryItems.Contains(trashtype))
        {
            //if the item is already in the inventory, then add to the amt
            Debug.Log("Add amount");
            //Debug.Log("its workin'")
            for(int i = 0; i < inventoryItems.Count; i++)
            {
                if(inventoryItems[i] == trashtype)
                {
                    amount[i]++;
                    images[i].GetComponent<InventoryButtons>().number.text = amount[i].ToString();
                    break;
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
        if(RoboInventory.roboInventory.inventoryItems.Count <= RoboInventory.roboInventory.capacity)
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
                                    int amtIndex = RoboInventory.roboInventory.inventoryItems.IndexOf(trashtype);
                                    RoboInventory.roboInventory.amount[amtIndex]++;
                                    //if the item is not already in the inventory, and it has an assigned sprite, then present the sprite and let it know the data of the thing in the slot
                                    RoboInventory.roboInventory.images[i].sprite = trashtype.inventorySprite;
                                    RoboInventory.roboInventory.images[i].GetComponent<RoboIntventoryButtons>().item = trashtype;
                                    RoboInventory.roboInventory.images[i].GetComponent<RoboIntventoryButtons>().selectable = true;
                                    RoboInventory.roboInventory.images[i].color = Color.white;
                                    RoboInventory.roboInventory.roboInventoryButtons[i].used = true;
                                    RoboInventory.roboInventory.roboInventoryButtons[i].number.text = RoboInventory.roboInventory.amount[amtIndex].ToString();
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
                    RoboInventory.roboInventory.roboInventoryButtons[amtIndex].number.text = RoboInventory.roboInventory.amount[amtIndex].ToString();
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
    public void PutInBinC(int i, Bin bin)
    {
        int ind = inventoryItems.IndexOf(selected[i].item);
        if(amount[ind] >= 1)
        {
            bin.inBin.Add(selected[i].item);
            amount[ind]--;
            selected[i].GetComponent<InventoryButtons>().number.text = amount[ind].ToString();
            if(amount[ind] == 0)
            {   
                inventoryItems.Remove(selected[i].item);                         
                selected[i].item = null;
                selected[i].GetComponent<Image>().sprite = null;
                selected[i].GetComponent<Image>().color = Color.red; 
                //selected[i].GetComponent<InventoryButtons>().number.text = amount[ind].ToString();
                selected[i].item = null;
            }
        }
        else if(amount[ind] == 0)
        {
            inventoryItems.Remove(selected[i].item);
            bin.inBin.Add(selected[i].item);
            selected[i].item = null;
            selected[i].GetComponent<Image>().sprite = null;
            selected[i].GetComponent<Image>().color = Color.red;
                            
            selected[i].GetComponent<InventoryButtons>().number.text = amount[ind].ToString();
            selected[i].item = null;
        }
        Debug.Log(ind);
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

                        PutInBinC(i, currentBin);
                    }
                    GameManager.instance.recyclePoints += 10;
                }
                else if(bin.binType == Bin.BinType.Garbage)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        PutInBinC(i, currentBin);
                    }
                }   
                else if(bin.binType == Bin.BinType.Organic)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        PutInBinC(i, currentBin);
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
                        PutInBinC(i, currentBin);
                    }
                    GameManager.instance.recyclePoints += 10;
                }
                else if(bin.binType == Bin.BinType.Organic)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        PutInBinC(i, currentBin);
                    }
                    GameManager.instance.recyclePoints -= 10;
                }
                else if(bin.binType == Bin.BinType.Recycle)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        PutInBinC(i, currentBin);
                    }
                    GameManager.instance.recyclePoints -= 10;
                }
                else if(bin.binType == Bin.BinType.Organic)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        PutInBinC(i, currentBin);
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
                        PutInBinC(i, currentBin);
                    }
                    GameManager.instance.recyclePoints += 10;
                }
                else if(bin.binType == Bin.BinType.Organic)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        PutInBinC(i, currentBin);
                    }
                    GameManager.instance.recyclePoints -= 10;
                }
                else if(bin.binType == Bin.BinType.Organic)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        PutInBinC(i, currentBin);
                    }
                    GameManager.instance.recyclePoints -= 10;
                }
                else if(bin.binType == Bin.BinType.Organic)
                {
                    if(inventoryItems.Contains(selected[i].item))
                    {
                        PutInBinC(i, currentBin);
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
    public void RemoveFromInvent(InventoryItem item)
    {
        if(inventoryItems.Contains(item))
        {
            int ind = inventoryItems.IndexOf(item);
            if(amount[ind] >= 1)
            {
                amount[ind]--;
            }
            else if(amount[ind] == 0)
            {
                //item.button = null;
                inventoryItems.Remove(item);
                item.button.GetComponent<Image>().sprite = null;
                item.button.GetComponent<Image>().color = Color.red;
                
            }
        }
    }
    public void PutInProcessor()
    {
        if(!currentMachine.broken)
        {
            for(int i = 0; i < selected.Count; i++)
            {
                if(selected[i].item.trashType != null)
                {
                    if(selected[i].item.trashType.recyclable)
                    {
                        if(currentMachine.contains.Count == 0 )
                        {
                            Debug.Log("IUB");
                            if(currentMachine.takes.HasFlag(selected[i].item.trashType.typeofMaterial))
                            {
                                
                                int ind = inventoryItems.IndexOf(selected[i].item);
                                if(amount[ind] == 0)
                                {
                                    Debug.Log(ind);
                                    Debug.Log("Process");
                                    inventoryItems.Remove(selected[i].item);
                                    currentMachine.PutIn(selected[i].item);
                                    selected[i].item = null;
                                    selected[i].GetComponent<Image>().sprite = null;
                                    selected[i].GetComponent<Image>().color = Color.red;
                                    selected[i].GetComponent<InventoryButtons>().number.text = amount[ind].ToString();
                                }
                                else if(amount[ind] >= 1)
                                {
                                    currentMachine.PutIn(selected[i].item);
                                    amount[ind]--;
                                    selected[i].GetComponent<InventoryButtons>().number.text = amount[ind].ToString();
                                    if(amount[ind] == 0)
                                    {
                                        Debug.Log(ind);
                                        Debug.Log("Process");
                                        inventoryItems.Remove(selected[i].item);
                                        //currentMachine.PutIn(selected[i].item);
                                        selected[i].item = null;
                                        selected[i].GetComponent<Image>().sprite = null;
                                        selected[i].GetComponent<Image>().color = Color.red;
                                        selected[i].GetComponent<InventoryButtons>().number.text = amount[ind].ToString();
                                    }
                                }
                            }
                        }
                        else if(currentMachine.contains.Count >= 1)
                        {
                            int ind = inventoryItems.IndexOf(selected[i].item);
                            if(selected[i].item.trashType == currentMachine.contains[0])
                            {
                                if(currentMachine.takes.HasFlag(selected[i].item.trashType.typeofMaterial))
                                {
                                    if(amount[ind] == 0)
                                    {
                                        Debug.Log("Process");
                                        inventoryItems.Remove(selected[i].item);
                                        currentMachine.PutIn(selected[i].item);
                                        selected[i].item = null;
                                        selected[i].GetComponent<Image>().sprite = null;
                                        selected[i].GetComponent<Image>().color = Color.red;
                                        selected[i].GetComponent<InventoryButtons>().number.text = amount[ind].ToString();
                                    }
                                    else if(amount[ind] >= 1)
                                    {
                                        currentMachine.PutIn(selected[i].item);
                                        amount[ind]--;
                                        selected[i].GetComponent<InventoryButtons>().number.text = amount[ind].ToString();
                                        if(amount[ind] == 0)
                                        {
                                            Debug.Log("K:JB");
                                            Debug.Log(ind);
                                            Debug.Log("Process");
                                            inventoryItems.Remove(selected[i].item);
                                            //currentMachine.PutIn(selected[i].item);
                                            selected[i].item = null;
                                            selected[i].GetComponent<Image>().sprite = null;
                                            selected[i].GetComponent<Image>().color = Color.red;
                                            selected[i].GetComponent<InventoryButtons>().number.text = amount[ind].ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {

        }
        selected.Clear();
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
        if(currentBin.following == false)
        {
            currentBin.following = true;
        }
        else
        {
            currentBin.following = false;
        }
    }
}
