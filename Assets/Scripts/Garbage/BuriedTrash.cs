using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuriedTrash : Trash
{
    public bool buried;
    public Rigidbody rb;
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        rb = GetComponent<Rigidbody>();
        buriedTrash = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(buried == true)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        }
        else 
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
    }
}
