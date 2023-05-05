using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : ProductionMachine
{
    // Start is called before the first frame update
    void Start()
    {
        furnace = this;
    }

    // Update is called once per frame
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
