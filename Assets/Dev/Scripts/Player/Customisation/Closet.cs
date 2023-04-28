using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour
{
    public List<ClothingAsset> clothes;
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
    public void Interact()
    {
        Clothing cloth = GameManager.instance.player.GetComponent<Clothing>();
        GameObject.Destroy(cloth.doneHair);
        cloth.hair = clothes[1].mesh;
        cloth.hairDone = false;
        cloth.OnValidate();
    }
}

