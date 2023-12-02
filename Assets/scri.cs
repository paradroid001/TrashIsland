using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrashIsland;

public class scri : MonoBehaviour
{
    public GameObject level2Load;
    public GameObject level2Unload;
    public Transform spawnPoint;
    public Transform altSpawnPoint;
    public Camera mainCamera;
    public Camera washingCamera;
    //public bool isReady;
 

    void LoadNewPoint()
    {
        Transform playerPos = FindObjectOfType<TempMovement>().transform;

        level2Load.SetActive(true);
        level2Unload.SetActive(false);
        /*
        mainCamera.gameObject.SetActive(false);
        washingCamera.gameObject.SetActive(true);
        */


        playerPos.position = spawnPoint.position;
    }
    public void LoadNewPoint1()
    {
        Transform playerPos = FindObjectOfType<TempMovement>().transform;

        level2Load.SetActive(false);
        level2Unload.SetActive(true);
        washingCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        mainCamera.gameObject.tag = "MainCamera";
        


        playerPos.position = altSpawnPoint.position;
    }
}
