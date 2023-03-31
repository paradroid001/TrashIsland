using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropButton : MonoBehaviour
{
    public Transform firstParent;
    void Start()
    {
        firstParent = transform.parent;
        gameObject.SetActive(false);
    }
}
