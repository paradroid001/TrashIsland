using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothing : MonoBehaviour
{
    public GameObject hair;
    public GameObject doneHair;
    public bool hairDone;
    public GameObject shirt;
    public GameObject pants;
    public GameObject shoes;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void OnValidate()
    {
        if(hairDone == false)
        {
            if(doneHair != null)
            {
                GameObject.Destroy(doneHair);
            }
            GameObject hairModel = GameObject.Instantiate<GameObject>(hair);
            doneHair = hairModel;
            hairModel.transform.SetParent(transform);
            hairModel.transform.localPosition = Vector3.zero;
            hairDone = true;
        }
    }
}
