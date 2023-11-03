using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExitTile : MonoBehaviour
{
    public int sceneRef = 0;
    public DemoManager gameManager;
    
    

   private void OnTriggerEnter(Collider other) 
   {
     if (other.tag == "Player")
     {
        Debug.Log("player entered trigger");
        gameManager.GetSceneToLoad(sceneRef);
     }
   }
}
