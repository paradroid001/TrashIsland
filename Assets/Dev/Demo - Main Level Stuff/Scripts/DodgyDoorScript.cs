using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgyDoorScript : MonoBehaviour
{
    [SerializeField]
    private Animator _Transition;

    public void SummonTransition()
    {
        _Transition.Play("washerFadeToBlack");
    }
}


