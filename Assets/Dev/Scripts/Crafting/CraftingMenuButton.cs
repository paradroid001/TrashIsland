using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CraftingMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public CraftingRecipe recipe;
    public List<GameObject> need = new List<GameObject>();
    public void OnPointerEnter(PointerEventData eventData)
    {
        //on hover, change the sprite and text for the crafting menu;
        if(recipe != null)
        {
            Image imageBox = CraftingMenuManager.instance.displayBox.GetComponent<Image>();
            imageBox.sprite = recipe.icon;
            need = CraftingMenuManager.instance.needed;
            for(int i = 0; i < need.Count; i++)
            {
                if(recipe.needed[i] != null)
                {
                    //Debug.Log("A");
                    need[i].GetComponent<TextMeshProUGUI>().text = recipe.needed[i].name;
                    need[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = recipe.amtneeded[i].ToString();
                }
                else
                {
                    Debug.Log("B");
                    need[i].GetComponent<TextMeshProUGUI>().text = string.Empty;
                }
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //on click, perform crafting. remove from the trash, add new thing to inventory
        for(int i = 0; i < recipe.needed.Count; i++)
        {
            if(GameManager.instance.invent.inventoryItems.Contains(recipe.needed[i]))
            {
                int amtHaveInd = GameManager.instance.invent.inventoryItems.IndexOf(recipe.needed[i]);
                int amtHave = GameManager.instance.invent.amount[amtHaveInd];
                if(amtHave >= recipe.amtneeded[i] - 1)
                {
                    GameManager.instance.invent.RemoveFromInvent(recipe.needed[i]);
                    GameManager.instance.invent.AddToInventory(recipe.makes);
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
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        for(int i = 0; i < need.Count; i++)
        {
            need[i].GetComponent<TextMeshProUGUI>().text = "";
            need[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "0";
        }
    }
}
