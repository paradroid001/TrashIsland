using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCaller : MonoBehaviour
{
    //just calls events when player collides
    public UnityEvent myOutput;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            myOutput.Invoke();
        }
    }
}
