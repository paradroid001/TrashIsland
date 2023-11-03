using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoManager : MonoBehaviour
{
    [SerializeField]
    private int gameStageRef;

    public enum SceneToLoad {outside = 0, inside = 1, minigame1 = 2, minigame2 = 3};
    SceneToLoad mySceneToLoad;

    [SerializeField]
    private GameObject transitionAnim;
    [SerializeField]
    private GameObject playerRef;

    bool sceneChangeActive;
    public int sceneIndex;

    public List<Transform> sceneSpawns;

    public List<GameObject> scenes;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug Testing
        /*
        if (Input.GetKeyDown(KeyCode.F) && sceneChangeActive == false)
        {
            StartCoroutine(ChangeScene());
        }
        */
    }

    public void GetSceneToLoad(int x)
    {
        //Checks for invalid scene number
        if(x > 3 || x < 0)
        {
            x = 0;
        }  

        sceneIndex = x;

        if (x == 0)
        {
            mySceneToLoad = SceneToLoad.outside;
        }

        if (x == 1)
        {
            mySceneToLoad = SceneToLoad.inside;
        }

        if (x == 2)
        {
            mySceneToLoad = SceneToLoad.minigame1;
        }

        if (x == 3)
        {
            mySceneToLoad = SceneToLoad.minigame2;
        }

        //Checks to see if scene is already in the process of changing
        if(sceneChangeActive == false)
        {
            StartCoroutine(ChangeScene());
        }
    }

    public IEnumerator ChangeScene()
    {       
        playerRef.GetComponent<TempMovement>().canMove = false;


        sceneChangeActive = true;
        transitionAnim.GetComponent<animationTracker>().StartAnimation();
        

        Debug.Log("Changing Scene");
        
        
        yield return new WaitForSeconds(1f);

        transitionAnim.GetComponent<animationTracker>().EndAnimation();
        //playerRef.GetComponent<TempMovement>().canMove = true;

        sceneChangeActive = false;
    }

    public void LevelChangeEvent()
    {
         for(int i = 0; i < scenes.Count; i++) // Disables all scenes that aren't active
        {
            playerRef.GetComponent<Rigidbody>().isKinematic = true;

        if (mySceneToLoad == SceneToLoad.inside || mySceneToLoad == SceneToLoad.outside)
        {
            playerRef.GetComponent<TempMovement>().ChangeLocation(sceneSpawns[sceneIndex]);
            playerRef.GetComponent<Rigidbody>().isKinematic = false;
        }
            if(i != sceneIndex)
            {
                scenes[i].SetActive(false);
            }
            else
            {
                scenes[i].SetActive(true);
            }
        }
    }
}
