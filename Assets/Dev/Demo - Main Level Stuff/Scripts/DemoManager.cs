using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace TrashIsland
{
[System.Serializable]
public class Minigame 
{
    public string name;
    public GameObject managerObject;
    public Camera camera;
}
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

    public List<Minigame> minigameList; //add minigames here

    [SerializeField]
    private Camera mainCam;
    private Minigame currentMinigame;

    [SerializeField]
    private GameObject _mainUI;


    // Start is called before the first frame update
    void Start()
    {
        transitionAnim.SetActive(true);
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
        playerRef.GetComponent<TrashIsland.TempMovement>().canMove = false;


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
            playerRef.GetComponent<TrashIsland.TempMovement>().ChangeLocation(sceneSpawns[sceneIndex]);
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



    public void StartMinigame(string gameName) //used to run the minigames
    {
        currentMinigame = null;
        foreach(Minigame mg in minigameList) //look for matching minigame by name
        {
            if(mg.name == gameName)
            {
                currentMinigame = mg; //asign the minigame
                break; //exit the loop early
            }
        }
        //disable player object (or teleport to a different spot temporarily)

        //enable conveyor game assets

        //disable any unwanted assets

        //start the conveyor minigame script

        currentMinigame.managerObject.SetActive(true);

        //switch camera
        mainCam.gameObject.SetActive(false);

        playerRef.GetComponent<TrashIsland.TempMovement>().DisableMovement();
        playerRef.SetActive(false);
        currentMinigame.camera.gameObject.SetActive(true);
        

    }

    public void ReturnToMain(Transform respawnPosition) //used for exiting a minigame
    {
        TempMovement tM = playerRef.GetComponent<TrashIsland.TempMovement>();
        //re-enable player

        //switch back to main camera
        currentMinigame.camera.gameObject.SetActive(false);
        _mainUI.SetActive(true);
        tM.ChangeLocation(respawnPosition);
        playerRef.SetActive(true);
        tM.EnableMovement();
        

        mainCam.gameObject.SetActive(true);

    }

    public void UIToggle(GameObject UIRef)
    {
       _mainUI.SetActive(false);
       UIRef.SetActive(true);
    }

    }
    
}
