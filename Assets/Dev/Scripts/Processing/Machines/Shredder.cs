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
        if(thing.plastic != null && thing.plastic.shredded == false && thing.recyclable == true)
        {
            switch(thing.plastic.plasticType)
            {
                case Plastic.PlasticType.Lid:
                    GameManager.instance.invent.AddToInventory(ProcessManager.instance.shreddedPlasticLid);
                    break;
                case Plastic.PlasticType.Container:
                    GameManager.instance.invent.AddToInventory(ProcessManager.instance.shreddedPlasticContainer);
                    break;
                case Plastic.PlasticType.Bottle:
                    GameManager.instance.invent.AddToInventory(ProcessManager.instance.shreddedPlasticBottle);
                    break;
            }
        }
        if(thing.metal != null && thing.metal.shredded == false && thing.recyclable == true)
        {
            switch(thing.metal.metalType)
            {
                case Metal.MetalType.Lid:
                    GameManager.instance.invent.AddToInventory(ProcessManager.instance.shreddedMetalLid);
                    break;
                case Metal.MetalType.Tin:
                    GameManager.instance.invent.AddToInventory(ProcessManager.instance.shreddedMetalTin);
                    break;
                case Metal.MetalType.Can:
                    GameManager.instance.invent.AddToInventory(ProcessManager.instance.shreddedMetalCan);
                    break;
            }
            //GameManager.instance.invent.AddToInventory(ProcessManager.instance.shreddedMetal);
        }
    }
}
