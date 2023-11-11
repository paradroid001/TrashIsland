using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    [SerializeField]
    private DemoManager demoManager;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            demoManager.SceneLoad(sceneName);
        }
    }
}
