using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrashIsland;

public class scri : MonoBehaviour
{
    public GameObject level2Load;
    public GameObject level2Unload;
    public Transform spawnPoint;
    public Camera mainCamera;
    public Camera washingCamera;
    public bool isReady;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider c)
    {
        TIInteractor t = c.GetComponent<TIInteractor>();
        if (t != null && isReady)
        {
            LoadNewPoint(t.gameObject);
        }
    }

    void LoadNewPoint(GameObject playerPos)
    {
        level2Load.SetActive(true);
        level2Unload.SetActive(false);
        mainCamera.gameObject.SetActive(false);
        washingCamera.gameObject.SetActive(true);


        playerPos.transform.position = spawnPoint.position;
    }
    public void LoadNewPoint1(GameObject playerPos)
    {
        level2Load.SetActive(false);
        level2Unload.SetActive(true);
        washingCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        


        playerPos.transform.position = spawnPoint.position;
    }
}
