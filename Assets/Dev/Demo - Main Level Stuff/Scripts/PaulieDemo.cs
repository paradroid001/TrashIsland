using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaulieDemo : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Quaternion defaultFacing;

    [SerializeField]
    private bool lookAt;
    [SerializeField]
    private bool resetGaze;
    [SerializeField]
    private Transform targetTransform;

    void Update()
    {
        //transform.LookAt(targetTransform.position);

        /*transform.rotation = Quaternion.LookRotation(targetTransform.position, Vector3.up);*/
    }

    /*
    void FixedUpdate() 
    {
        
        if(lookAt)
        {
            resetGaze = false;
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if(resetGaze)
        {
            lookAt = false;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, defaultFacing, rotationSpeed * Time.deltaTime);
        }
        
    }


    private void OnTriggerEnter(Collider col) 
    {
        if(col.CompareTag("Player"))
        {
            LookAt(col.transform);
        }
    }
    void OnTriggerExit(Collider col) 
    {
        if(col.CompareTag("Player"))
        {
            BreakGaze();
        }
    }



    private void LookAt(Transform player) //Turns to look at object transform that has been passed to function (instead of snapping and locking onto it)
    {
        targetTransform = player.transform;
        defaultFacing = transform.rotation;
        lookAt = true;       
        
    }

    private void BreakGaze()
    {
        lookAt = false;
        resetGaze = true;
    }
    */
}
