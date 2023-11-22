using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMenu : MonoBehaviour
{
    bool hasBegun;

    [SerializeField]
    private GameObject menu;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasBegun)
        {
            gameObject.GetComponent<Animator>().Play("DemoGameStart");
            hasBegun = true;
        }
    }

    public void EndMenu()
    {
        menu.SetActive(false);
    }
}
