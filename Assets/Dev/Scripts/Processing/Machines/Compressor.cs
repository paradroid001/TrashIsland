using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compressor : ProductionMachine
{
    void Start()
    {
        compressor = this;
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
        if(thing.glass != null && thing.glass.crushed == false && thing.glass.clean == false && thing.glass.recyclable == true)
        {
            switch(thing.glass.glassType)
            {
                case Glass.GlassType.Jar:
                GameManager.instance.invent.AddToInventory(ProcessManager.instance.glassJarCompressed);
                    break;
                case Glass.GlassType.Bottle:
                GameManager.instance.invent.AddToInventory(ProcessManager.instance.glassBottleCompressed);
                    break;
            }
        }
    }
    
}
