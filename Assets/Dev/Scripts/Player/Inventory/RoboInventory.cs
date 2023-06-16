using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoboInventory : MonoBehaviour
{
    public int capacity;
    public static RoboInventory roboInventory;
    public enum RoboInventoryReason{Not, Look, Bin, Process}
    public RoboInventoryReason reason;
    public bool organise;
    public GameObject menu;
    public List<Image> images;
    public int[] amount; 
    public List<InventoryItem> inventoryItems;
    public List<RoboIntventoryButtons> roboInventoryButtons;
    public List<GameObject> lines;
    void Start()
    {
        roboInventory = this;
        menu = transform.GetChild(0).GetChild(7).gameObject;
        roboInventoryButtons = new List<RoboIntventoryButtons>();
        for(int i = 0; i < menu.transform.GetChild(0).childCount; i++)
        {
            lines.Add(menu.transform.GetChild(0).GetChild(i).gameObject);
            for(int j = 0; j < menu.transform.GetChild(0).GetChild(i).childCount; j++)
            {
                images.Add(menu.transform.GetChild(0).GetChild(i).GetChild(j).GetComponent<Image>());
                roboInventoryButtons.Add(menu.transform.GetChild(0).GetChild(i).GetChild(j).GetComponent<RoboIntventoryButtons>());
            }
        }
    }
    public void UpdateRoboInventory()
    {
        
    }
    public void Return(GameObject leave)
    {
        reason = RoboInventoryReason.Not;
        organise = false;
        GameManager.instance.invent.organise = false;
        leave.SetActive(false);
        Time.timeScale = 1;
    }
    void Update()
    {
        switch(reason)
        {
            case RoboInventoryReason.Not:
                menu.SetActive(false);
                break;
            case RoboInventoryReason.Look:
                menu.SetActive(true);
                break;
            case RoboInventoryReason.Bin:
                menu.SetActive(true);
                break;
            case RoboInventoryReason.Process:

                break;
        }
    }
}
