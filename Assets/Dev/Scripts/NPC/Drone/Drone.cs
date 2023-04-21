using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : NPC
{
    public bool sent;
    public Bin bin;
    public enum WhichBin{Recycle, Landfill, Organic};
    public WhichBin whichBin;
    public List<InventoryItem> carrying;
    void Start()
    {
        DroneManager.instance.drones.Add(this);
    }
    void Update()
    {
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Bin>() == bin)
        {
            carrying = bin.inBin;
        }
    }
    public void GoToBin(Bin bin)
    {
        agent.SetDestination(bin.transform.position);
    }
}
