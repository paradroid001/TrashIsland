using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CraftingMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public bool selected;
    public CraftingRecipe recipe;
    public GameObject child;
    public List<GameObject> need = new List<GameObject>();
    public List<InventoryItem> needUse;
    public List<int> needUseAMT;
    public void OnValidate()
    {
        //child = transform.GetChild(0).gameObject;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //on hover, change the sprite and text for the crafting menu;
        
    }
public int neededAmt = 0;
public int amtHaveVsNeed = 0;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(selected == false)
        {
            if(recipe != null)
            {
                
                selected = true;
                Image imageBox = CraftingMenuManager.instance.displayBox.GetComponent<Image>();
                imageBox.sprite = recipe.icon;
                need = CraftingMenuManager.instance.needed;
                for(int i = 0; i < recipe.needed.Count; i++)
                {
                    needUse.Add(recipe.needed[i]);
                    needUseAMT.Add(recipe.amtneeded[i]);
                }
                
                for(int i = 0; i <= need.Count; i++)
                {
                    if(recipe.needed[i] != null)
                    {
                        Debug.Log("A");
                        need[i].GetComponent<TextMeshProUGUI>().text = recipe.needed[i].name;
                        need[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = recipe.amtneeded[i].ToString();
                        //GameManager.instance.invent.inventoryItems.Remove(recipe.needed[i]);
                    }
                    else if(recipe.needed[i] == null)
                    {
                        Debug.Log("B");
                        need[i].GetComponent<TextMeshProUGUI>().text = string.Empty;
                    }
                    else
                    {
                        Debug.Log("C");
                    }
                }
                
            }
            else
            {
                Debug.Log("Don't Craft");
            }
        }
        else
        {
            
            /*for(int i = 0; i < recipe.needed.Count; i++)
            {
                neededAmt += recipe.amtneeded[i];
            }*/
            //on click, perform crafting. remove from the trash, add new thing to inventory
           for(int i = 0; i <= recipe.needed.Count; i++)
            {
                if(GameManager.instance.invent.inventoryItems.Contains(recipe.needed[i]))
                {
                    Debug.Log("Have Item");
                    if(needUseAMT[i] > 1)
                    {
                        Debug.Log("Have More than 1");
                        needUseAMT[i]--;
                    }
                    else if(needUseAMT[i] == 1)
                    {
                        Debug.Log("Have 1");
                        needUseAMT[i]--;
                        needUse[i] = null;
                    }
                    if(i == recipe.needed.Count - 1 && needUse[i] == null)
                    {
                        
                        for(int j = 0; j < recipe.needed.Count; j++)
                        {
                            int amtHaveInd = GameManager.instance.invent.inventoryItems.IndexOf(recipe.needed[j]);
                            Debug.Log("Craft");
                            if(GameManager.instance.invent.amount[amtHaveInd] == 0)
                            {
                                Debug.Log("Remove");
                                GameManager.instance.invent.inventoryItems[amtHaveInd] = null;
                                GameManager.instance.invent.amount[amtHaveInd]--;
                                GameManager.instance.invent.images[amtHaveInd].sprite = null;
                                GameManager.instance.invent.images[amtHaveInd].GetComponent<InventoryButtons>().item = null;
                                GameManager.instance.invent.images[amtHaveInd].color = Color.red;
                                
                            }
                            else
                            {
                                Debug.Log("Remove1");
                                //GameManager.instance.invent.amount[amtHaveInd]--;
                                GameManager.instance.invent.RemoveFromInvent(recipe.needed[i]);
                                GameManager.instance.invent.images[amtHaveInd].GetComponent<InventoryButtons>().number.text = GameManager.instance.invent.amount[amtHaveInd].ToString();
                                if(GameManager.instance.invent.amount[amtHaveInd] == 0)
                                {
                                    GameManager.instance.invent.inventoryItems[amtHaveInd] = null;
                                    //GameManager.instance.invent.amount[amtHaveInd]--;
                                    GameManager.instance.invent.images[amtHaveInd].sprite = null;
                                    GameManager.instance.invent.images[amtHaveInd].GetComponent<InventoryButtons>().item = null;
                                    GameManager.instance.invent.images[amtHaveInd].color = Color.red;
                                }
                            }
                        }
                        if(neededAmt == amtHaveVsNeed)
                        {
                            GameManager.instance.invent.AddToInventory(recipe.makes);
                            Debug.Log("Z");
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    Debug.Log("Don't have");
                }
                /*if(GameManager.instance.invent.inventoryItems.Contains(recipe.needed[i]))
                {
                    amtHaveVsNeed++;
                    int amtHaveInd = GameManager.instance.invent.inventoryItems.IndexOf(recipe.needed[i]);
                    int amtHave = GameManager.instance.invent.amount[amtHaveInd];
                    if(amtHave >= recipe.amtneeded[i])
                    {
                        
                        if(GameManager.instance.invent.amount[amtHaveInd] == 0)
                        {
                            GameManager.instance.invent.inventoryItems[amtHaveInd] = null;
                            GameManager.instance.invent.amount[amtHaveInd]--;
                            GameManager.instance.invent.images[amtHaveInd].sprite = null;
                            GameManager.instance.invent.images[amtHaveInd].GetComponent<InventoryButtons>().item = null;
                            GameManager.instance.invent.images[amtHaveInd].color = Color.red;
                        }
                        else
                        {
                            //GameManager.instance.invent.amount[amtHaveInd]--;
                            GameManager.instance.invent.RemoveFromInvent(recipe.needed[i]);
                            GameManager.instance.invent.images[amtHaveInd].GetComponent<InventoryButtons>().number.text = GameManager.instance.invent.amount[amtHaveInd].ToString();
                            if(GameManager.instance.invent.amount[amtHaveInd] == 0)
                            {
                                GameManager.instance.invent.inventoryItems[amtHaveInd] = null;
                                //GameManager.instance.invent.amount[amtHaveInd]--;
                                GameManager.instance.invent.images[amtHaveInd].sprite = null;
                                GameManager.instance.invent.images[amtHaveInd].GetComponent<InventoryButtons>().item = null;
                                GameManager.instance.invent.images[amtHaveInd].color = Color.red;
                            }
                        }
                        GameManager.instance.invent.RemoveFromInvent(recipe.needed[i]);
                        Debug.Log("V");
                        if(neededAmt == amtHaveVsNeed)
                        {
                            GameManager.instance.invent.AddToInventory(recipe.makes);
                            Debug.Log("Z");
                        }
                    }
                    else
                    {
                        Debug.Log("A");
                        return;
                    }
                }
                else
                {
                    Debug.Log("B");
                }*/
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        /*for(int i = 0; i < need.Count; i++)
        {
            need[i].GetComponent<TextMeshProUGUI>().text = "";
            need[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "0";
        }*/
    }
}
