using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compressor : ProductionMachine
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(interacting == true && interacted == false)
        {
            StartCoroutine(Interact());
        }
    }
    
}
