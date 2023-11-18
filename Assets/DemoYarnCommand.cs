using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.AI;

namespace TrashIsland
{

public class DemoYarnCommand : MonoBehaviour
{
    [Header("NavMesh Agents")]
    [Space(5)]
    [SerializeField]
    private NavMeshAgent Paulie;
    [SerializeField]
    private NavMeshAgent Sparc;
    [Space(20)]

    [Header("NavMesh Targets")]
    [Space(5)]
    [SerializeField]
    private List<Transform> PaulieTransforms;
    [SerializeField]
    private List<Transform> SparcTransforms;

[YarnCommand("makeNPCWalk")]
  public void InterpretDestination(string agentName, int positionRef)
  {
    NavMeshAgent agent;
    Transform destination;

    if (agentName == "Paulie")
    {
        //Paulie is target       
        agent = Paulie;

        //Paulie.SetDestination
        if(positionRef <= PaulieTransforms.Count && positionRef >= 0)
        {
            destination = PaulieTransforms[positionRef];
            SetNPCDestination(agent, destination);
        }
    }
    
    if (agentName == "Sparc")
    {
        //Sparc is target
        agent = Sparc;

        //Paulie.SetDestination
        if(positionRef <= SparcTransforms.Count && positionRef >= 0)
        {
            destination = SparcTransforms[positionRef];            
            SetNPCDestination(agent, destination);
        }
    }
  }

  [YarnCommand("LoadLevel")]
  public void YarnLoadLevel(int levelRef) 
  {
      gameObject.GetComponent<DemoManager>().GetSceneToLoad(levelRef);
  }

  void SetNPCDestination(NavMeshAgent nvma, Transform trans)
  {
    // Gets relevant scripts from the transform object and the agent
    Demo_InteractableNPC npc = nvma.gameObject.GetComponent<Demo_InteractableNPC>();
    NPCStopper stopper = trans.gameObject.GetComponent<NPCStopper>();

    //Sets NPC on their course
    nvma.enabled = true;
    nvma.SetDestination(trans.position);

    //Disables interaction until the stopper at the destination reactivates it
    npc.InMotion();
    stopper.AwaitForNPC(npc);    
  }
}
}