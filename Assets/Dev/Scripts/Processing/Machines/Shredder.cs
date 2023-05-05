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
        if(thing.plastic != null && thing.plastic.shredded == false)
        {
            GameManager.instance.invent.AddToInventory(ProcessManager.instance.cleanPlastic);
        }
        if(thing.metal != null && thing.metal.shredded == false)
        {
            GameManager.instance.invent.AddToInventory(ProcessManager.instance.shreddedMetal);
        }
    }
}
