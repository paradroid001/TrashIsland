using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager instance;
    public List<InventoryItem> inInventory;
    public List<CraftingRecipe> craftingRecipes;
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        
    }
    //interact stuff
    public void Interact()
    {
        GameManager.instance.craftingMenu.SetActive(true);
        CraftingMenuManager.instance.UpdateMenu();
        Time.timeScale = 0;
    }
    public void UnlockRecipe(CraftingRecipe recipe)
    {
        craftingRecipes.Add(recipe);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.interactable = true;
            player.Interactables.Add(gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.interactable = false;
            player.Interactables.Remove(gameObject);
        }
    }
}
