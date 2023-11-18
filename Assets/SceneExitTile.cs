using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
public class SceneExitTile : MonoBehaviour
{
   [SerializeField]
    private int sceneRef;
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
}
