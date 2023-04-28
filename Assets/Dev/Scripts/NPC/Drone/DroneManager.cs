using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneManager : MonoBehaviour
{
    public static DroneManager instance;
    public List<Drone> drones;
    public RecycleDeposit recycleDeposit;
    public Compost compost;
    public Vector3 landfill;
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
                switch(bin.binType)
                {
                    case Bin.BinType.Recycle:
                        drones[i].whichBin = Drone.WhichBin.Recycle;
                    break;
                    case Bin.BinType.Garbage:
                        drones[i].whichBin = Drone.WhichBin.Landfill;
                    break;
                    case Bin.BinType.Organic:
                        drones[i].whichBin = Drone.WhichBin.Organic;
                    break;
                }
                drones[i].agent.SetDestination(bin.transform.position);
                break;
            }
        }
    }
}
