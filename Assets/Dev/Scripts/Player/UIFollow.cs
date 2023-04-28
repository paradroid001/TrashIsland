using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIFollow : MonoBehaviour
{
    public Transform player;
    public Canvas canvas;
    public List<GameObject> buttons;
    public int number;
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + 1.8f, player.position.y + 0.5f, player.position.z);
        if(GameManager.instance.player.Interactables.Count > 0)
        {
            if(Input.mouseScrollDelta.y <= -0.1f)
            {
                number++;
            }
            else if(Input.mouseScrollDelta.y >= 0.1f)
            {
                number--;
            }
        }
        if(GameManager.instance.inDialogue == false)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                transform.GetChild(0).GetChild(number).GetComponent<Button>().onClick.Invoke();
                number = 0;
            }
        }
        if(transform.GetChild(0).childCount > 3)
        {
            Destroy(transform.GetChild(0).GetChild(3).gameObject);
        }
    }
}
