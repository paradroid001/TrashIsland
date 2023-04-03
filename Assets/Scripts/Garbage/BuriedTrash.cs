using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuriedTrash : Trash
{
    public bool buried;
    public bool near;
    public Image img;
    public Sprite sprite;
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
            if(near)
            {
                img.sprite = sprite;
                img.color = Color.white;
            }
            else
            {
                img.sprite = null;
                img.color = Color.clear;
            }
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        else 
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }
    }
}
