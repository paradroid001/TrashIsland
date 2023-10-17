using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConveyorMinigame : MonoBehaviour
{
    /*NOTES:
    This script will only exist in the scene of the minigame.
    If the minigame is not loaded as a separate scene then it will get turned on/off or instantated TBC.

    Planning to have the minigame work with the regular objects that exist in the rest of the game
    So whatever interactable / rigid body scripts they have will need to be left on, but not interfere.

    This also means the movement controls of those objects will just be managed by this mini game script. So that there is not 'conveyor' functions on all of these objects in the regular scenes and in inventory.

    */

    //variable parameters
    public List<GameObject> itemsList; //the items that have been put into the recepticle for this minigame
    private bool gameRunning;
    public float gameSpeed; //overall speed multiplier - starts at 1
    public float beltSpeed; //the speed the items move from left to right
    public float spawnRate; //frequency objects appear in seconds
    private float gameTimer; //record of time in the game
    private float timestampSpawn; //when the last object was spawned
    public Vector3 beltDirection; //the direction that the belt moves

    //varaible references   
    [SerializeField]
    private ConveyorUI ui; //the UI specifically related to this minigame
    [SerializeField]
    private Transform conveyorParent; //holds all objects on the conveyor belt
    [SerializeField]
    private Transform conveyorSpawn; //where the items spawn
    [SerializeField]
    private Transform conveyorDespawn; //where the items de - spawn at the end of the belt
    [SerializeField]
    private Transform conveyorHopper; //where items inside this machine are stored

    void Start()
    {
        //upon start, initiate UI screen that gives a start or exit button to the player
        gameRunning = false;
        GameCountdown(); //temp code until UI is set up
        
    }

    void GameCountdown() //start a routine of couting down numbers on the screen, before the mini game commences
    {
        GameStart();
    }

    void GameStart() //start the core fo the game, relying on update checks to then end it
    {
        GameInitialise();
        foreach(Transform child in conveyorHopper) //add all the items in the hopper to the list
        {
            itemsList.Add(child.gameObject);
        }
    }

    void GameInitialise() //set up all beginning variables and references
    {
        gameRunning = true;
        gameTimer = 0.0f;   
        timestampSpawn = 0.0f;
        itemsList = new List<GameObject>();
    }
    void EndCheck() //checks to see if end conditions are met
    {
        if(false)
        {
            GameEnd();
        }
    }
    void GameEnd() //called when the game is over to move to end screens
    {
        gameRunning = false;
        GameExit();
    }

    void GameExit() //called to leave this scene
    {

    }

    void Update()
    {
        if(gameRunning)
        {
            gameTimer += Time.deltaTime;
            SpawnItems();
            MoveItems();
            PlayerInput();
            EndCheck();
        }
    }

    void SpawnItems() //spawn objects at the left of the conveyor
    {
        if(gameTimer - timestampSpawn >= spawnRate/gameSpeed) //if <time> has passed since last spawn
        {
            timestampSpawn = gameTimer; //time stamp the current time
            //spawn the first item in the list
            GameObject spawnItem = new GameObject();
            if(itemsList.Count > 0) //use list until it is empty
            {
                spawnItem = itemsList[0];
                itemsList.Remove(itemsList[0]); //remove the first item from the list
            }
            else
            {
                return; //return if both lists are empty
            }
            //GameObject spawned = Instantiate(spawnItem, conveyorSpawn.position, Quaternion.identity, conveyorParent); //spawn the object
            spawnItem.transform.position = conveyorSpawn.position; //move the object to the spawn pos
            spawnItem.transform.parent = conveyorParent; //set the object's parent
            spawnItem.SetActive(true); //enable the object
        }
    }

    void MoveItems() //move items along the conveyor
    {
        foreach(Transform child in conveyorParent) //iterates through each item that was spawned under the conveyor parent
        {
            child.position += beltDirection.normalized * Time.deltaTime * beltSpeed * gameSpeed; //move that object
            if(child.position.x >= conveyorDespawn.position.x) //if the item has then reached the end of the belt
            {
                itemsList.Add(child.gameObject); //adds the prefab that the item instanced from back into the rotation
                child.parent = conveyorHopper; //put it back in the hopper
                child.gameObject.SetActive(false); //hide the item
            }
        }

    }

    void PlayerInput() //receive touch controls to grab items
    {
        //detect where on screen player touching

        //detect if touching an item

        //detect if dragging an item

    }

    void GrabItem(GameObject item) //the code that is enacted onto the grabbed item
    {
        //remove item from conveyor

        //have it follow finger


    }

    void DropItem(GameObject item) //the code that is enacted onto the dropped item
    {
        //detect where it was dropped

        //accept sorted item

        //reject misorted item

        //put it back on the conveyor

    }




}
