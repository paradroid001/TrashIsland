using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bin : MonoBehaviour
{
    public enum BinType {Garbage, Recycle, Organic};
    public BinType binType;
    public List<InventoryItem> inBin;
    public Animator anim;
    public bool hasThings;
    public bool sent;
    public bool interact;
    public NavMeshAgent agent;
    public bool following;
    public Transform sparc;
    public void Start()
    {
        sparc = GameManager.instance.sparc;
        agent = transform.GetComponent<NavMeshAgent>();
    }
    public void Update()
    {
        if(following)
        {
            agent.SetDestination(GameManager.instance.player.transform.position);
            if(agent.remainingDistance <= 7)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
        if(inBin.Capacity > 1)
        {
            hasThings = true;
        }
        if(interact)
        {
            anim.Play("Bin Open");
            anim.SetBool("Open", true);
        }
        else
        {
            anim.SetBool("Open", false);
        }
    }
    public GameObject button;
}
