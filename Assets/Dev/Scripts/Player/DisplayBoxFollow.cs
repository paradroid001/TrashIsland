using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBoxFollow : MonoBehaviour
{
    public Vector3 mousePos;
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3 (worldPosition.x + .01f, worldPosition.y, worldPosition.z + .0001f);
    }
}
