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
        recipeBox = menu.transform.GetChild(0).GetChild(1).gameObject;
        displayBox = recipeBox.transform.GetChild(0).GetChild(0).gameObject;
        for(int i = 1; i < recipeBox.transform.GetChild(0).childCount; i++)
        {
            //get the text objects
            needed.Add(recipeBox.transform.GetChild(0).GetChild(i).gameObject);
        }
        for(int i = 0; i < menu.transform.GetChild(0).GetChild(0).childCount; i++)
        {
            Transform Lines = menu.transform.GetChild(0).GetChild(0);
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
        bool done = false;
        for(int i = 0; i < buttons.Capacity; i++)
        {
            if(buttons[i].recipe == null && done == false)
            {
                buttons[i].recipe = CraftingManager.instance.craftingRecipes[i];
                Image img = buttons[i].GetComponent<Image>();
                img.sprite = CraftingManager.instance.craftingRecipes[i].icon;
                done = true;
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
