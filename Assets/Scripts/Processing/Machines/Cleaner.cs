using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : ProductionMachine
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(interacting == true)
        {
            StartCoroutine(Interact());
        }
    }
    public IEnumerator Interact()
    {
        yield return new WaitForSeconds(3);
    }
}
