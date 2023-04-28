using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Companion : NPC
{
    public Transform player;
    void Awake()
    {
    }
    void Update()
    {
        agent.SetDestination(player.position);
        if(agent.remainingDistance < 5)
        {
            agent.isStopped = true;
        }
        else 
        {
            agent.isStopped = false;
        }
    }
}
