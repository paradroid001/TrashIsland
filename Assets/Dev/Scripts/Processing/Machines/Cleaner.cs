using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : ProductionMachine
{
    // Start is called before the first frame update
    void Start()
    {
        cleaner = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(interacting == true)
        {
            StartCoroutine(Interact());
        }
    }
    public void DoThing(TrashType thing)
    {
        if(thing.plastic != null && thing.plastic.clean == false)
        {
            GameManager.instance.invent.AddToInventory(ProcessManager.instance.cleanPlastic);
        }
        if(thing.metal != null && thing.metal.shredded == true && thing.plastic.clean == false)
        {
            GameManager.instance.invent.AddToInventory(ProcessManager.instance.cleanMetal);
        }
    }
}
