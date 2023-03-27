using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        
    }
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 5.5f, player.position.z - 7f);
    }
}
