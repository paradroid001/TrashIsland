using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessManager : MonoBehaviour
{
    public static ProcessManager instance;
    public MaterialItem plastic;
    public Plastic plasticLid;
    public Plastic plasticBottle;
    public Plastic plasticContainer;
    public Plastic cleanPlastic;
    public Plastic cleanPlasticLid;
    public Plastic cleanPlasticBottle;
    public Plastic cleanPlasticContainer;
    public Plastic shreddedPlastic;
    public Plastic shreddedPlasticLid;
    public Plastic shreddedPlasticBottle;
    public Plastic shreddedPlasticContainer;
    public MaterialItem metal;
    public Metal metalLid;
    public Metal metalCan;
    public Metal metalTin;
    public Metal shreddedMetal;
    public Metal shreddedMetalLid;
    public Metal shreddedMetalCan;
    public Metal shreddedMetalTin;
    public Metal cleanMetal;
    public Metal cleanMetalLid;
    public Metal cleanMetalCan;
    public Metal cleanMetalTin;
    public Glass glassBottle;
    public Glass glassJar;
    public Glass glassJarCompressed;
    public Glass glassBottleCompressed;
    public Glass glassJarWashed;
    public Glass glassBottleWashed;
    void Start()
    {
        instance = this;
    }
}
