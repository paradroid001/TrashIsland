using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager instance;
    public List<InventoryItem> inInventory;
    public List<CraftingRecipe> craftingRecipes;
    GameObject button;
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
        //CraftingMenuManager.instance.UpdateMenu();
        Time.timeScale = 0;
        GameManager.instance.player.GetComponent<TrashIsland.TICharacterMovement>().enabled = false;
    }
    public void UnlockRecipe(CraftingRecipe recipe)
    {
        craftingRecipes.Add(CraftingList.instance.manuals[CraftingList.instance.whichOne]);
        CraftingList.instance.whichOne++;
        CraftingMenuManager.instance.UpdateMenu();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            player.interactable = true;
            player.Interactables.Add(gameObject);
            button = GameObject.Instantiate<GameObject>(GameManager.instance.player.interactButtonTemplate);
            button.transform.SetParent(GameManager.instance.player.playerCanvas.transform.GetChild(0));
            button.transform.localPosition = Vector2.zero;
            TextMeshProUGUI buttontext = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttontext.text = "Crafting";
            InteractButtons interactButton = button.GetComponent<InteractButtons>();
            interactButton.correspond = gameObject;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            player.interactable = false;
            player.Interactables.Remove(gameObject);
            GameObject.Destroy(button);
        }
    }
}
