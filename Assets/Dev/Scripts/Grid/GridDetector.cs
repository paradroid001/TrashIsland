using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDetector : MonoBehaviour
{
    public List<GridSquare> inFront;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Floor")
        {
            inFront.Add(other.GetComponent<GridSquare>());
        }
    }
    public void OnTriggerExit (Collider other)
    {
        if(other.tag == "Floor")
        {
            inFront.Remove(other.GetComponent<GridSquare>());
        }
    }
}
