using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
    
    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        
    }
}
