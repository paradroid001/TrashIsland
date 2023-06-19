using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PickupEvent : MonoBehaviour
{
    public UnityEvent pickupChange;
    GameObject button;
    public string title;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        //transform.position += new Vector3(1, 1, 1);
        pickupChange.Invoke();
        GameManager.instance.player.Interactables.Remove(this.gameObject);
        gameObject.SetActive(false);
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
            buttontext.text = "Crafting Manual";
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
