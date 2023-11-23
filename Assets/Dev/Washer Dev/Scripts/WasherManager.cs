using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrashIsland;

public class WasherManager : MonoBehaviour
{
    enum GameState {Tutorial, Play, End};


    [Header ("Inventories")]
    [Space(10)]
    public Transform dirtyInventory;
    [Space(5)]
    public Transform cleanInventory;
    [Space(5)]
    public Transform washingInventory;
    [Space(20)]




    [Header("Stored References")]
    [Space(5)]

    [SerializeField]
    private DemoManager gM;
    [SerializeField]
    private GameObject myUI;
    [SerializeField]
    private GameObject myCam;

    [Space(5)]

    [SerializeField]
    private Material baseMaterial;
    [Space(5)]

    [SerializeField]
    private Material dirtyMaterial;
    [Space(5)]

    public Transform spawnPosition;
    [Space(20)]




    [Header("Minigame Variables")]
    [Space(10)]
    public List<GameObject> potentialItems;
    [SerializeField]
    [Space(5)]
    private List<Transform> endPositions;
    [Space(5)]
    public List<GameObject> liveItemsList;
    [Space(5)]
    public GameObject currentObject = null;
    [Space(5)]
    public int currentCount;
    [Space(5)]
    public bool isCleaning = false;
    [Space(5)]
    public float itemCleanliness;
    [Space(5)]
    public float transitionTime = 2f;
    [Space(5)]
    public int itemCount;
    [SerializeField]
    private bool IsActiveMinigame;
    
    bool pregame = true;

    public List<GameObject> itemVariations;

    

    void OnEnable() 
    
    {
        /*
        IsActiveMinigame = true;
        gM.UIToggle(myUI);
        gM.SwapCameras(myCam, myCam);   
        */     

    }

    // Update is called once per frame
    void Update()
    {
        if (IsActiveMinigame)
        {
            if (isCleaning)
            { 
            if (Input.GetKey(KeyCode.E))
            {
                //Debug.Log("cleaning");
                itemCleanliness += Time.deltaTime;
            }

            if (itemCleanliness >= 5f)
            {
                Debug.Log("cleaning complete");

                // Moves object position from cleaning to cleaned inventory
                currentObject.transform.position = cleanInventory.position;

                currentObject.GetComponent<WashPaint>().isTarget = false;
                // Sets cleaned inventory as parent
                currentObject.transform.SetParent(cleanInventory);

                isCleaning = false;

                StartCoroutine(itemTransition());
            }
            }
            if (pregame && Input.GetKeyDown(KeyCode.Space))
            {
                pregame = false;
                SetupGame();
            }
        }

        

       

    }

    public void itemFullyCleaned()
    {
        // Moves object position from cleaning to cleaned inventory
                currentObject.transform.position = endPositions[currentCount].position;

                //currentObject.GetComponent<WashPaint>().isTarget = false;
                // Sets cleaned inventory as parent
                currentObject.transform.SetParent(cleanInventory);

                isCleaning = false;

                StartCoroutine(itemTransition());
    }

    public void SetupGame()
    {
        pregame = false;
        
        
        foreach(Transform child in dirtyInventory)
        {
             liveItemsList.Add(child.gameObject);
        }

        //liveItemsArray = liveItemsList.ToArray();
        itemCount = liveItemsList.Count;

        foreach(GameObject g in liveItemsList)
        {
            Renderer r = g.GetComponent<Renderer>();
            r.material = dirtyMaterial;
            g.layer = 2;
        }
        //liveItemsList = null;
        //Debug.Log(itemCount + " items were found");

        currentCount = 0;
        StartGame();
    }

    void StartGame()
    {
        currentObject = liveItemsList[currentCount];

        CleaningTime();

    }

    void CleaningTime()
    {
        itemCleanliness = 0;



        currentObject.GetComponent<Renderer>().material = baseMaterial; // Sets the item material to the washer material
        WashPaint wP = currentObject.GetComponent<WashPaint>();
        wP.Setup(this); //Sets all of the values up on the washer texture
        wP.isTarget = true;
        currentObject.layer = 0;

        currentObject.transform.position = spawnPosition.position;
        currentObject.transform.rotation = spawnPosition.rotation;
        currentObject.transform.SetParent(washingInventory);

        isCleaning = true;
    }

    public IEnumerator itemTransition()
    {
        yield return new WaitForSeconds(transitionTime);

        if (currentCount >= (itemCount - 1))
        {
            WinGame();
        }
        else
        {
            currentCount++;
            StartGame();
        }

    }

    void WinGame()
    {
        if (currentCount >= itemCount - 1)
        {
            Debug.Log("Won");
        }
    }
}
