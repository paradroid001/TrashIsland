using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : ProductionMachine
{
    void Start()
    {
        shredder = this;
    }
    void Update()
    {
        if(interacting == true && interacted == false)
        {
            StartCoroutine(Interact());
        }
    }
    public void DoThing(TrashType thing)
    {
        if(thing.plastic != null)
        {
            GameManager.instance.invent.AddToInventory(ProcessManager.instance.cleanPlastic);
        }
        if(thing.metal != null)
        {
            GameManager.instance.invent.AddToInventory(ProcessManager.instance.cleanMetal);
        }
    }
}
