using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoboInventory : MonoBehaviour
{
    public int capacity;
    public static RoboInventory roboInventory;
    public GameObject menu;
    public List<Image> images;
    public int[] amount; 
    public List<InventoryItem> inventoryItems;
    public List<RoboIntventoryButtons> roboInventoryButtons;
    public List<GameObject> lines;
    void Start()
    {
        roboInventory = this;
        menu = transform.GetChild(2).gameObject;
        roboInventoryButtons = new List<RoboIntventoryButtons>();
        for(int i = 0; i < transform.GetChild(2).GetChild(0).childCount; i++)
        {
            lines.Add(transform.GetChild(2).GetChild(0).GetChild(i).gameObject);
            for(int j = 0; j < transform.GetChild(2).GetChild(0).GetChild(i).childCount; j++)
            {
                images.Add(transform.GetChild(2).GetChild(0).GetChild(i).GetChild(j).GetComponent<Image>());
                roboInventoryButtons.Add(images[j].GetComponent<RoboIntventoryButtons>());
            }
        }
    }
    public void UpdateRoboInventory()
    {
        
    }
    public void Return(GameObject leave)
    {
        leave.SetActive(false);
        Time.timeScale = 1;
    }
    void Update()
    {
        
    }
}
