using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public List<GameObject> itemsIn; //the items that have been put into the recepticle for this minigame
    private List<GameObject> itemsLap; //items are put in here when they are going through the conveyor multiple times
    private bool gameRunning;

    //varaible references   
    [SerializeField]
    private ConveyorUI ui; //the UI specifically related to this minigame

    void Start()
    {
        //upon start, initiate UI screen that gives a start or exit button to the player
        gameRunning = false;
        
    }

    void GameCountdown() //start a routine of couting down numbers on the screen, before the mini game commences
    {
        GameStart();
    }

    void GameStart() //start the core fo the game, relying on update checks to then end it
    {
        gameRunning = true;        
    }
    void EndCheck() //checks to see if end conditions are met
    {
        if(true)
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
            SpawnItems();
            MoveItems();
            PlayerInput();
            EndCheck();
        }
    }

    void SpawnItems() //spawn objects at the left of the conveyor
    {


    }

    void MoveItems() //move items along the conveyor
    {

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
