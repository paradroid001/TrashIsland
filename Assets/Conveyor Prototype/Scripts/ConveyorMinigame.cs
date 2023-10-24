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

    //Game Management
    private bool gameRunning;
    private float gameTimer; //record of time in the game

    //Belt Spawning and Movement
    public List<GameObject> itemsList; //the items that have been put into the recepticle for this minigame
    public float gameSpeed; //overall speed multiplier - starts at 1
    public float beltSpeed; //the speed the items move from left to right
    public float spawnRate; //frequency objects appear in seconds
    private float timestampSpawn; //when the last object was spawned
    public Vector3 beltDirection; //the direction that the belt moves
    [SerializeField]
    private Transform conveyorParent; //holds all objects on the conveyor belt
    [SerializeField]
    private Transform conveyorSpawn; //where the items spawn
    [SerializeField]
    private Transform conveyorDespawn; //where the items de - spawn at the end of the belt
    [SerializeField]
    private Transform conveyorHopper; //where items inside this machine are stored

    //Flicking Objects
    private Rigidbody flickObject; //the object we are interacting with
    private Vector2 touchStartPos, touchEndPos, swipeDirection; //touch input vectors
    private float touchStartTime, touchEndTime, swipeDuration; //touch input times
    [SerializeField]
    private float flickForceX; //how far to the side object flicks
    [SerializeField]
    private float flickForceY; //how far to the up the object flicks
    [SerializeField]
    private float flickForceZ; //how far back the object flicks

    //UI  
    [SerializeField]
    private ConveyorUI ui; //the UI specifically related to this minigame

    

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
        //touch begins
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) //if we are touching the screen and have just begun the touch
        {
            touchStartTime = Time.time; //timestamp
            touchStartPos = Input.GetTouch(0).position; //pos stamp

            //cast to see if we pick up an item
            Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); //cast a ray from where we touch
            RaycastHit hit;
            if(Physics.Raycast(touchRay, out hit))
            {
                if(hit.collider != null && hit.collider.tag == "Prop"); //if we hit something that is a prop
                {
                    if(hit.collider.GetComponentInParent<Rigidbody>()) //if there is a rigid body to grab
                    {
                        flickObject = hit.collider.GetComponentInParent<Rigidbody>(); //assign the object we hit to our flick object
                    }
                }
            }
        }

        //touch ends
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) //if we are touching the screen and have just ended the touch
        {
            if(!flickObject) return; //if there was no target object, we don't need any of this
            touchEndTime = Time.time; //timestamp
            touchEndPos = Input.GetTouch(0).position; //pos stamp

            swipeDirection = touchStartPos - touchEndPos; //convert 2 positions to a direction
            swipeDuration = touchEndTime - touchStartTime; //convert 2 times to time difference

            flickObject.isKinematic = false; //apply the swipe as force to the flick object
            flickObject.AddForce(-swipeDirection.x * flickForceX, -swipeDirection.y * flickForceY, flickForceZ / swipeDuration); //shorter swipe interval, means stronger flick backwards

            flickObject = null; //reset the flick Object
        }


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
