using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform playerPosition;

    public GameObject playerRef;
    public GameObject cameraTarget;

    // Start is called before the first frame update
    void Start()
    {
        //playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = playerRef.transform;

        transform.parent.position = playerPosition.position;

        transform.LookAt(cameraTarget.transform);
    }
    
}
