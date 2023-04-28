using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : NPC
{
    public Vector3 startPoint;
    public bool sent;
    public Bin bin;
    public enum WhichBin{Recycle, Landfill, Organic};
    public WhichBin whichBin;
    public List<InventoryItem> carrying;
    void Awake()
    {
        DroneManager.instance.drones.Add(this);
        startPoint = transform.position;
    }
    void Update()
    {
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Recycle" || other.tag == "Garbage" || other.tag == "Organic")
        {
            if(other.GetComponent<Recycle>() != null && other.GetComponent<Recycle>() == bin && other.isTrigger == false)
            {
                //play pickup animmation
                carrying = bin.inBin;
                agent.SetDestination(DroneManager.instance.recycleDeposit.transform.position);
                if(agent.remainingDistance <= 0.005f)
                {
                    //play drop animation
                    agent.SetDestination(startPoint);
                }
            }
            else if(other.GetComponent<Garbage>() != null && other.GetComponent<Garbage>() == bin && other.isTrigger == false)
            {
                //play pick up animation
                carrying = bin.inBin;
                agent.SetDestination(DroneManager.instance.landfill);
                if(agent.remainingDistance <= 0.05)
                {
                    //play drop animation
                    agent.SetDestination(startPoint);
                }
            }
            else if(other.GetComponent<Organics>() != null && other.GetComponent<Organics>() == bin && other.isTrigger == false)
            {
                //play pickup animation
                carrying = bin.inBin;
                agent.SetDestination(DroneManager.instance.compost.transform.position);
                if(agent.remainingDistance <= 0.005f)
                {
                    //play drop animation
                    agent.SetDestination(startPoint);
                }
            }
        }
    }
    public void GoToBin(Bin bin)
    {
        agent.SetDestination(bin.transform.position);
    }
}
