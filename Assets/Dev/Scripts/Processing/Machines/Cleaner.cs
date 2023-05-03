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
        if(thing.plastic != null && thing.plastic.shredded == true && thing.plastic.clean == false && thing.recyclable == true)
        {
            switch(thing.plastic.plasticType)
            {
                case Plastic.PlasticType.Lid:
                    GameManager.instance.invent.AddToInventory(ProcessManager.instance.cleanPlasticLid);
                    break;
                case Plastic.PlasticType.Container:
                    GameManager.instance.invent.AddToInventory(ProcessManager.instance.cleanPlasticContainer);
                    break;
                case Plastic.PlasticType.Bottle:
                    GameManager.instance.invent.AddToInventory(ProcessManager.instance.cleanPlasticBottle);
                    break;
            }
        }
        else if(thing.metal != null && thing.metal.shredded == true && thing.metal.clean == false && thing.recyclable == true)
        {
            Debug.Log(thing.name);
            switch(thing.metal.metalType)
            {
                case Metal.MetalType.Lid:
                    GameManager.instance.invent.AddToInventory(ProcessManager.instance.cleanMetalLid);
                    break;
                case Metal.MetalType.Tin:
                    GameManager.instance.invent.AddToInventory(ProcessManager.instance.cleanMetalTin);
                    break;
                case Metal.MetalType.Can:
                    GameManager.instance.invent.AddToInventory(ProcessManager.instance.cleanMetalCan);
                    break;
            }
        }
    }
}
