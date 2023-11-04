using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasherManager : MonoBehaviour
{
    enum GameState {Tutorial, Play, End};

    public Transform dirtyInventory;
    public Transform cleanInventory;
    public Transform washingInventory;

    public Transform spawnPosition;
    public List<GameObject> potentialItems;

    public List<GameObject> liveItemsList;
    //public GameObject[] liveItemsArray;
    public GameObject currentObject = null;
    public int currentCount;

    public bool isCleaning = false;
    public float itemCleanliness;
    public float transitionTime = 2f;

    public bool pregame = true;

    public int itemCount;

    // Start is called before the first frame update
    void Start()
    {
        //SetupGame();
    }

    // Update is called once per frame
    void Update()
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

    void SetupGame()
    {
        
        
        foreach(Transform child in dirtyInventory)
        {
             liveItemsList.Add(child.gameObject);
        }

        //liveItemsArray = liveItemsList.ToArray();
        itemCount = liveItemsList.Count;
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

        currentObject.transform.position = spawnPosition.position;
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
