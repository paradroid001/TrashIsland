using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneManager : MonoBehaviour
{
    public static DroneManager instance;
    public List<Drone> drones;
    public void OnValidate()
    {
        instance = this;
    }
    void Start()
    {
    }
    void Update()
    {
        
    }
    public void SendToBin(Bin bin)
    {
        for(int i = 0; i <= drones.Capacity; i++)
        {
            if(drones[i].sent == false)
            {
                Debug.Log("0");
                drones[i].bin = bin;
                drones[i].agent.SetDestination(bin.transform.position);
                break;
            }
        }
    }
}
