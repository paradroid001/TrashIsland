using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Paulie : NPC
{
    public Transform sparc;
    public bool following = true;
    void Start()
    {
    }
    void Update()
    {
        if(sparc != null)
        {
            agent.destination = sparc.position;
            if(agent.remainingDistance <= 0.7f)
            {
                agent.isStopped = true;
            }
            else if(agent.remainingDistance > 0.5f)
            {
                agent.isStopped = false;
            }
        }
    }
}
