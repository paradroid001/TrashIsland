using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Paulie : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform sparc;
    public bool following = true;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if(sparc != null)
        {
            agent.destination = sparc.position;
            if(agent.remainingDistance <= 0.5f)
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
