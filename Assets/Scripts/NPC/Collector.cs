using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : NPC
{
    public enum CurrentStatus {Wander, Collecting, Returning};
    public CurrentStatus currentStatus;
    public bool holding;
    public Transform hold;
    public Trash whichTrash;
    public bool collected; 
    void Start()
    {
        hold = transform.GetChild(0);
        StartCoroutine(Collect());
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStatus)
        {
            case CurrentStatus.Wander:
                WhileWandering();
                break;
            case CurrentStatus.Collecting:
                WhileCollecting();
                break;
            case CurrentStatus.Returning:
                WhileReturning();
                break;
        }
    }
    public IEnumerator Collect()
    {
        yield return new WaitForSeconds(10);
        int whichBehaviour = Random.Range(0, 2);
        if(whichBehaviour == 0)
        {
            currentStatus = CurrentStatus.Wander;
        }
        if(whichBehaviour == 1)
        {
            currentStatus = CurrentStatus.Collecting;
        }
    }
    public void WhileWandering()
    {
        //if () ;
    }
    public bool trashChosen;
    public void WhileCollecting()
    {
        if (trashChosen == false)
        {
            int whichOne = Random.Range(0, GameManager.instance.trash.Capacity);
            whichTrash = GameManager.instance.trash[whichOne];
            trashChosen = true;
        }
        else return;
        agent.SetDestination(whichTrash.transform.position);
        if(collected == true)
        {
            currentStatus = CurrentStatus.Returning;
        }
    }
    public void WhileReturning()
    {
        if(collected == true)
        {
            Trash heldTrash = transform.GetChild(0).GetChild(0).GetComponent<Trash>();
            if(heldTrash.trashType == Trash.TrashType1.recycle)
            {
                agent.SetDestination(GameManager.instance.recycles[0].transform.position);
            }
            else if (heldTrash.trashType == Trash.TrashType1.garbage)
            {
                agent.SetDestination(GameManager.instance.garbages[0].transform.position);
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(currentStatus == CurrentStatus.Collecting)
        {
            if(collision.collider.gameObject.tag == "Trash")
            {
                whichTrash.NPCInteract(this);
                trashChosen = false;
                currentStatus = CurrentStatus.Returning;
            }
        }
        if (currentStatus == CurrentStatus.Returning)
        {
            Trash trashHeld = hold.GetChild(0).GetComponent<Trash>();
            if (collision.collider.gameObject.tag == "Recycle" && trashHeld.trashType == Trash.TrashType1.recycle)
            {
                collision.collider.gameObject.GetComponent<Recycle>().Interact();
                GameManager.instance.trash.Remove(trashHeld);
                Destroy(hold.GetChild(0).gameObject);
                collected = false;
                holding = false;
                currentStatus = CurrentStatus.Wander;
            }
            if (collision.collider.gameObject.tag == "Garbage" && trashHeld.trashType == Trash.TrashType1.garbage)
            {
                collision.collider.gameObject.GetComponent<Garbage>().Interact();
                GameManager.instance.trash.Remove(trashHeld);
                Destroy(hold.GetChild(0).gameObject);
                collected = false;
                holding = false;
                currentStatus = CurrentStatus.Wander;
            }
        }
    }
}
