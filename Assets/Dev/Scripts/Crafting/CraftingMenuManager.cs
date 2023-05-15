using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMenuManager : MonoBehaviour
{
    public static CraftingMenuManager instance;
    public GameObject menu;
    public List<GameObject> lines;
    public List<GameObject> boxes;
    public GameObject recipeBox;
    public GameObject displayBox;
    public List<GameObject> needed;
    public List<CraftingMenuButton> buttons;
    void Start()
    {
        instance = this;
        menu = transform.GetChild(1).gameObject;
        recipeBox = menu.transform.GetChild(0).GetChild(2).gameObject;
        displayBox = recipeBox.transform.GetChild(1).GetChild(0).gameObject;
        for(int i = 0; i < recipeBox.transform.GetChild(0).childCount; i++)
        {
            //get the text objects
            needed.Add(recipeBox.transform.GetChild(0).GetChild(i).gameObject);
        }
        for(int i = 0; i < menu.transform.GetChild(0).GetChild(1).childCount; i++)
        {
            Transform Lines = menu.transform.GetChild(0).GetChild(1);
            lines.Add(Lines.GetChild(i).gameObject);
            for(int j = 0; j < lines[i].transform.childCount; j++)
            {
                // get the buttons from the crafting menu
                boxes.Add(lines[i].transform.GetChild(j).gameObject);
                buttons.Add(lines[i].transform.GetChild(j).GetComponent<CraftingMenuButton>());
            }
        }
    }
    public void UpdateMenu()
    {
        //change the icons when you get new crafting recipes
        for(int i = 0; i < buttons.Capacity; i++)
        {
            if(buttons[i].recipe == null)
            {
                buttons[i].recipe = CraftingManager.instance.craftingRecipes[i];
                Image img = buttons[i].child.GetComponent<Image>();
                img.sprite = CraftingManager.instance.craftingRecipes[i].icon;
                break;
            }
        }
    }
    public void ExitMenu(GameObject menu)
    {
        //leave the menu
        menu.SetActive(false);
        Time.timeScale = 1;
    }
}
