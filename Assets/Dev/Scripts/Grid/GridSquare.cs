using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquare : MonoBehaviour
{
    public bool built;
    public List<GridSquare> neighbours;
    void Start()
    {

    }
    public void OnTriggerStay(Collider other) 
    {
        if(other.tag == "Floor")
        {
            if(!neighbours.Contains(other.gameObject.GetComponent<GridSquare>()))
            {
                neighbours.Add(other.gameObject.GetComponent<GridSquare>());
            }
        }
    }
    void Update()
    {
        
    }
}
