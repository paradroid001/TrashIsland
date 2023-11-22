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
    private GameObject mainCam;
    private Minigame currentMinigame;

    [SerializeField]
    private GameObject _mainUI;
    [SerializeField]
    private GameObject _menuUI;
    [SerializeField]
    private GameObject menuCam;


    [SerializeField]
    private Demo_InteractableNPC Paulie;  // Don't judge me, I've not slept in 3 days and I need to get this working by tonight -_-


    // Start is called before the first frame update
    void Start()
    {
        //transitionAnim.SetActive(true);
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
        transitionAnim.GetComponent<Animator>().Play("SceneTransitionOut");
        

        Debug.Log("Changing Scene");
        
        
        yield return new WaitForSeconds(1f);
        DemoYarnCommand dYC = gameObject.GetComponent<DemoYarnCommand>();
        dYC.SceneTransitionNPC(sceneIndex); //Passes scene number to Yarn Command Manager for NPC changes between scenes
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

    public void StartMenu()
    {
        TempMovement tM = playerRef.GetComponent<TempMovement>();
        
        tM.enabled = false;
        tM.GetComponent<Rigidbody>().isKinematic=true;
        tM.transform.rotation = new Quaternion(0,0.766044497f,0.642787635f,0);

        Paulie.gameObject.GetComponent<Collider>().enabled=false;
        
        tM.CallAnimation("Sleep");
        _menuUI.SetActive(true);
        _mainUI.SetActive(false);

        SwapCameras(mainCam, menuCam);
        

    }

    public void endMenu()
    {
        TempMovement tM = playerRef.GetComponent<TempMovement>();
         _menuUI.SetActive(false);
        _mainUI.SetActive(true);

        tM.transform.rotation = new Quaternion(0,0,0,1);
        tM.enabled = true;
        tM.anim.Play("Idle");
        tM.aEP.ResetTexture();
        tM.GetComponent<Rigidbody>().isKinematic=false;

        Paulie.gameObject.GetComponent<Collider>().enabled=true;

        SwapCameras(menuCam, mainCam);
        mainCam.SetActive(true);
        menuCam.SetActive(false);
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

        playerRef.GetComponent<TrashIsland.TempMovement>().DisableMovement();
        playerRef.SetActive(false);
        
        

    }

    public void ReturnToMain(Transform respawnPosition, GameObject miniCam) //used for exiting a minigame
    {
        TempMovement tM = playerRef.GetComponent<TrashIsland.TempMovement>();
        //re-enable player

        //switch back to main camera
        miniCam.SetActive(false);
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
    
    public void SwapCameras(GameObject cameraOld, GameObject cameraNew)
    {
        cameraOld.tag = "Untagged";
        cameraOld.SetActive(false);

        cameraNew.tag = "MainCamera";
        cameraNew.SetActive(true);
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }


    }
    
}
