using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
public class AnimEventPasser : MonoBehaviour
{
    [SerializeField]
    private string animToCall;

    [SerializeField]
    private Texture shutEye;
    [SerializeField]
    private Texture face;
    [SerializeField]
    private Renderer rend;

    [SerializeField]
    private TempMovement target;

    [SerializeField]
    private bool determiner;
    public void passToParent()
    {
        target.EnableMovement();
    }

    public void DestroyInteractable()
    {
        target.DeleteObject();
    }

    public void SetBool()
    {
       //target.movementOverride = false;
    }

    public void SetExit()
        {
            GetComponent<Animator>().SetBool("canExit", true);
        }

    public void SetTexture()
    {
        rend.material.SetTexture("_BaseMap", shutEye);
    }

    public void ResetTexture()
    {
        rend.material.SetTexture("_BaseMap", face);
    }

    public void StartGame()
    {
        DemoManager dM = FindObjectOfType<DemoManager>();
        dM.endMenu();
    }

    public void StartDialogue()
    {
        TempMovement player = FindObjectOfType<TempMovement>();
        player.EnterDialogue();
    }

    public void LoadLevel()
    {
        DemoManager dM = FindObjectOfType<DemoManager>();
        dM.LevelChangeEvent();
    }

    public void CallTransitionAnim()
    {
        Animator tAnim = FindObjectOfType<GameEnd>().GetComponent<Animator>();
        tAnim.Play(animToCall);
    }
}
}