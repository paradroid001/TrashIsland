using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
public class animationTracker : MonoBehaviour
{
    private Animator myAnimator;
    
    public TrashIsland.TempMovement playerControls;
    public DemoManager gameManager;

    void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            myAnimator = GetComponent<Animator>();
        }
    }
    public void StartAnimation()
    {
        myAnimator.SetFloat("transitionState", 1f);
    }

    public void EndAnimation()
    {
        myAnimator.SetFloat("transitionState", 0f);
    }

    public void FreezePlayer()
    {
        playerControls.canMove = false;
    }

    public void UnfreezePlayer()
    {
        playerControls.canMove = true;
    }

    public void SwapLevels()
    {
        gameManager.LevelChangeEvent();
    }
}
}