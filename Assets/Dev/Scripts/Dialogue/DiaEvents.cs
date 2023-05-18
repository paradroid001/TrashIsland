using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class DiaEvents
{
    [SerializeField] private UnityEvent diaEvent;
    public List<int> align;
    public UnityEvent DiaEvent => diaEvent;
    public DialogueOBJ diaOBJ;
    public void OnValidate() 
    {

    }
}
