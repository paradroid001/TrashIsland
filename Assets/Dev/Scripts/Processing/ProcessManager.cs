using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessManager : MonoBehaviour
{
    public static ProcessManager instance;
    public Plastic plastic;
    public Plastic cleanPlastic;
    public Plastic shreddedPlastic;
    public Metal metal;
    public Metal shreddedMetal;
    public Metal cleanMetal;
    void Start()
    {
        instance = this;
    }
}
