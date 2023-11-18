using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace TrashIsland{


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
    [SerializeField]
    private DemoManager gameManager;
    [SerializeField]
    private GameObject minigameCamera;
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private Transform playerSpawn; //passes this to manager when minigame ends 
    [Space(20)]

    private bool gameRunning;
    private float gameTimer; //record of time in the game
    private int numToSort;
    private int numSorted;

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
    public Material beltShader;

    //Flicking Objects
    private Rigidbody flickObject; //the object we are interacting with
    private Vector3 touchStartPos, touchEndPos, swipeDirection; //touch input vectors
    private float touchStartTime, touchEndTime, swipeDuration; //touch input times
    [SerializeField]
    private float flickForceX; //how far to the side object flicks
    [SerializeField]
    private float flickForceY; //how far to the up the object flicks
    [SerializeField]
    private float flickForceZ; //how far back the object flicks
    [SerializeField]
    private float flickMinY; //minimum upward force applied to flicks
    [SerializeField]
    private Transform airParent; //the parent for when objects are flicked into the air

    //receptacles dealing with objects
    public float rejectionTranslationSpeed; //how fast the objeects return to the belt
    public float rejectionRotationSpeed;
    public float rejectionArc; //the vertical distance applied

    //UI  
    [SerializeField]
    private ConveyorUI ui; //the UI specifically related to this minigame
    [SerializeField]
    private Animator txt_Go;

    //Hugo - Fine Tuning Tweaks 

    [SerializeField]
    Transform binR;
    [SerializeField]
    Vector2 binRScreenPos;
    [SerializeField]
    Transform binG; 
    [SerializeField]
    Vector2 binGScreenPos;
    [SerializeField]
    Transform binY;
    [SerializeField]
     Vector2 binYScreenPos;
     private Vector3 targetPosition;
     private Vector3 launchDirection;

    

    void OnEnable()
    {
        //upon start, initiate UI screen that gives a start or exit button to the player
        gameRunning = false;

        minigameCamera.SetActive(true);
        mainCamera.SetActive(false);

        gameManager.UIToggle(ui.gameObject);
        
        
        //ui.gameObject.SetActive(true); //set cause the UI to appear - swapped for function that simultaniously keeping the EventManager active
        
        binRScreenPos = Camera.main.WorldToScreenPoint(binR.position);
        binGScreenPos = Camera.main.WorldToScreenPoint(binG.position);
        binYScreenPos = Camera.main.WorldToScreenPoint(binY.position);
    }

    public void GameStart() //start the core of the game, relying on update checks to then end it
    {
        GameInitialise();
        
        foreach(Transform child in conveyorHopper) //add all the items in the hopper to the list
        {
            itemsList.Add(child.gameObject);
            numToSort = itemsList.Count;
        }
    }

    void GameInitialise() //set up all beginning variables and references
    {
        gameRunning = true;
        gameTimer = spawnRate/2; //jump the game timer so that the first object spawns immediately on game start   
        timestampSpawn = 0.0f;
        itemsList = new List<GameObject>();
    }
    void EndCheck() //checks to see if end conditions are met
    {
        if(numSorted >= numToSort)
        {
            GameEnd();
        }
    }
    void GameEnd() //called when the game is over to move to end screens
    {
        Debug.Log("GameEnd");
        gameRunning = false;
        ui.endScreen.gameObject.SetActive(true);
        ui.endScreen.Play("PanelSlideDown");
    }

    public void GameExit() //called to leave this scene
    {
        Debug.Log("Game Exit");
        ui.gameObject.SetActive(false); //dsiable the UI
        gameManager.ReturnToMain(playerSpawn, minigameCamera); //call return method in main manager
        gameObject.SetActive(false); //disable this object

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
            beltShader.SetFloat("_Speed", beltSpeed * gameSpeed * 0.5f); //match the speed of the texture to the speed of the objects
        }
        else
        {
            beltShader.SetFloat("_Speed", 0.0f); //match the speed of the texture to the speed of the objects
        }

        
    }

    void SpawnItems() //spawn objects at the left of the conveyor
    {
        if(gameTimer - timestampSpawn >= spawnRate/gameSpeed) //if <time> has passed since last spawn
        {
            timestampSpawn = gameTimer; //time stamp the current time
            
            if(itemsList.Count > 0) //only spawn an object if there is something to spawn
            {
                GameObject spawnItem = itemsList[0]; //spawn the first item in the list
                itemsList.Remove(itemsList[0]); //remove the first item from the list
                spawnItem.transform.position = conveyorSpawn.position; //move the object to the spawn pos
                spawnItem.transform.parent = conveyorParent; //set the object's parent
                spawnItem.SetActive(true); //enable the object
            }
            
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

                Rigidbody rb = child.GetComponentInParent<Rigidbody>(true);
                if(rb = flickObject) //if this object is the same one we are storing for flicking
                {
                    flickObject = null; //reset flick object
                }
            }
        }
    }

    void PlayerInput() //receive mouse controls to grab items
    {
        //click begins
        if(Input.GetMouseButtonDown(0)) //if we are touching the screen and have just begun the touch
        {
            touchStartTime = Time.time; //timestamp
            touchStartPos = Input.mousePosition; //pos stamp

            //cast to see if we pick up an item
            Ray touchRay = Camera.main.ScreenPointToRay(touchStartPos); //cast a ray from where we touch
            RaycastHit hit;
            if(Physics.Raycast(touchRay, out hit))
            {
                if(hit.collider != null && hit.collider.tag == "Prop" && hit.transform.IsChildOf(conveyorParent)) //if we hit something that is a prop and is under the conveyor parent
                {
                    if(hit.collider.GetComponentInParent<Rigidbody>()) //if there is a rigid body to grab
                    {
                        flickObject = hit.collider.GetComponentInParent<Rigidbody>(); //assign the object we hit to our flick object
                    }
                }
            }
        }

        //touch ends
        if(Input.GetMouseButtonUp(0)) //if we release left click
        {
            if(!flickObject) return; //if there was no target object, we don't need any of this
            touchEndTime = Time.time; //timestamp
            touchEndPos = Input.mousePosition; //pos stamp

            swipeDirection = touchEndPos - touchStartPos; //convert 2 positions to a direction
            swipeDuration = touchEndTime - touchStartTime; //convert 2 times to time difference

            /*
            float redAngle = Vector2.Angle(touchStartPos, binRScreenPos); //Stores angles for swipe angle + angle to each target  
            float greenAngle = Vector2.Angle(touchStartPos, binGScreenPos);
            float yellowAngle = Vector2.Angle(touchStartPos, binYScreenPos);    
            float swipeAngle = Vector2.Angle(touchStartPos, touchEndPos);
            
            redAngle = Mathf.Abs(redAngle-swipeAngle);  // checks which angle was the closest
            greenAngle = Mathf.Abs(greenAngle-swipeAngle);
            yellowAngle = Mathf.Abs(yellowAngle-swipeAngle);

            if (redAngle < greenAngle && redAngle < yellowAngle)
                {
                    launchDirection = binR.position - flickObject.position;
                }
            if (greenAngle < redAngle && greenAngle < yellowAngle)
            {
                launchDirection = binG.position - flickObject.position;
            }
            else
            {
                launchDirection = binY.position - flickObject.position;
            }
            float targetDistance = Vector3.Distance(flickObject.position, targetPosition);
            */




            flickObject.isKinematic = false; //apply the swipe as force to the flick object
            //flickObject.AddForce(launchDirection.x * targetDistance * 100, launchDirection.y * 1.5f * 100, launchDirection.z * targetDistance * 100);

            flickObject.AddForce(swipeDirection.x * flickForceX, swipeDirection.y * flickForceY + flickMinY, flickForceZ / swipeDuration); //shorter swipe interval, means stronger flick backwards

            flickObject.transform.parent = airParent; //the object is in the air, not the hopper or the belt
            
            flickObject = null; //reset the flick Object
        }
    }

    public void AcceptItem() //called when an item is accepted by receptacle
    {
        //insert any effects that occur each time
        numSorted ++;
        EndCheck();
    }

    public IEnumerator RejectItem(Transform item)
    {
        item.parent = airParent;
        item.GetComponent<Rigidbody>().isKinematic = true; //set the item to kinematic
        float totalDistance = Vector3.Distance(item.position, conveyorParent.position); //how far to the belt - used to calculate arc
        while(Vector3.Distance(item.position, conveyorParent.position) > 0.05f)//while the object has not returned to the belt
        {
            Vector3 direction = conveyorParent.position - item.position;
            //affect y direction based on comparison to total distance
            if(Vector3.Distance(item.position, conveyorParent.position) > totalDistance/2) //if are less than halfway to the belt
            {
                item.position += new Vector3(0, rejectionArc, 0) * Time.deltaTime; //apply upward y movement
            }
            item.position += direction.normalized * rejectionTranslationSpeed * Time.deltaTime; //translate object back to belt over time
            item.rotation = Quaternion.RotateTowards(item.rotation, conveyorParent.rotation, rejectionRotationSpeed * Time.deltaTime); //rotate object back to 0,0,0 over time
            yield return null;
        }
        item.position = conveyorParent.position; //teleport to be exactly at position
        item.rotation = conveyorParent.rotation; //reset to 0 rotation
        item.parent = conveyorParent; //put the item back under the belt parent
    }

    void OnDisable()
    {
        beltShader.SetFloat("_Speed", 0.0f); //match the speed of the texture to the speed of the objects
    }
}
}
