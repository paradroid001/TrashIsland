using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBehaviourNPC : MonoBehaviour
{
    [SerializeField]
    private List<Transform> SceneLocations;

    [SerializeField]
    private List<bool> ActiveInScene;

    public void ChangeScene(int i)
    {
        if (i<= SceneLocations.Count && i<= ActiveInScene.Count && i>= 0)
        {
            gameObject.transform.position = SceneLocations[i].position;
            gameObject.SetActive(ActiveInScene[i]);     
        }
        else
        Debug.Log("Error: "+gameObject.name+" is missing references for loaded scene");                   
    }
}
