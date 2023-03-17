using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compressor : ProductionMachine
{
    // Start is called before the first frame update
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
    public IEnumerator Interact()
    {
        interacted = true;
        yield return new WaitForSeconds(3);
        Debug.Log("Compressed");
        for(int i = 0; i < contains.Capacity; i++)
        {
            GameManager.instance.materials.Add(produces);
        }
        interacting = false;
    }
}
