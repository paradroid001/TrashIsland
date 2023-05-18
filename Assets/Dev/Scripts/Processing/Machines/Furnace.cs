using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : ProductionMachine 
{
    public bool repaired;
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
        if(thing.plastic != null && thing.plastic.shredded && thing.plastic.clean && thing.recyclable == true)
        {
            GameManager.instance.invent.AddToInventory(thing.materialItemA);
        }
        else if(thing.metal != null && thing.metal.shredded && thing.metal.clean && thing.recyclable == true)
        {
            GameManager.instance.invent.AddToInventory(thing.materialItemA);
        }
        else if(thing.glass != null && thing.glass.crushed && thing.glass.clean && thing.recyclable == true)
        {
            GameManager.instance.invent.AddToInventory(thing.materialItemA);
        }
    }
}
