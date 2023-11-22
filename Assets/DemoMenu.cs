using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrashIsland;

public class DemoMenu : MonoBehaviour
{
    bool hasBegun;

    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject mainUI;
    [SerializeField]
    private AnimEventPasser aEP;

    [SerializeField]
    private Animator sceneTransition;
    

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
        mainUI.SetActive(true);
        GetComponent<Animator>().Play("CinematicCameraIntro");
    }

    public void AwakenPlayer()
    {
        aEP.ResetTexture();
    }

    public void SetPlayerSleeping()
    {
        Animator a = aEP.GetComponent<Animator>();
        a.Play("Sleep");
        aEP.SetTexture();
    }

    public void EndCinematic()
    {
        sceneTransition.Play("WhiteFadeIn");
    }
}
