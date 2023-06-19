using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone : NPC
{
    public Vector3 startPoint;
    public bool sent;
    public bool got;
    public Bin bin;
    public enum WhichBin{Recycle, Landfill, Organic};
    public WhichBin whichBin;
    public List<InventoryItem> carrying;
    void Awake()
    {
        DroneManager.instance.drones.Add(this);
        //agent = transform.GetChild(0).GetComponent<NavMeshAgent>();
        startPoint = transform.position;
    }
    void Update()
    {
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Recycle" || other.tag == "Garbage" || other.tag == "Organic")
        {
            if(other.tag == "Recycle" && other.GetComponent<Recycle>() == bin && other.isTrigger == false)
            {
                Debug.Log("POIU");
                //play pickup animmation
                carrying = bin.inBin;
                //agent.baseOffset = 0;
                agent.SetDestination(DroneManager.instance.recycleDeposit.transform.position);
                got = true;
                //agent.baseOffset = 0.11f;
                if(agent.remainingDistance <= 0.05f)
                {
                    //play drop animation
                    
                }
            }
            else if(other.GetComponent<RecycleDeposit>() != null)
            {
                carrying.Clear();
                agent.SetDestination(startPoint);
            }
            else if(other.tag == "Garbage" && other.GetComponent<Garbage>() == bin && other.isTrigger == false)
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
            else if(other.tag == "Organic" && other.GetComponent<Organics>() == bin && other.isTrigger == false)
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
