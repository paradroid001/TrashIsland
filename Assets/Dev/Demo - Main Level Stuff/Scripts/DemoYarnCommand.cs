using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.AI;
using UnityEngine.UI;

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
    
    [Space(20)]

    [Header("Reference Objects")]

    [SerializeField]
    private Sprite DialoguePortrait;

    [SerializeField]
    private Image LivePortrait;
    
    [SerializeField]
    private TempMovement playerScript;

    
    [SerializeField]
    public List<Demo_InteractableNPC> activeNPCList;





//Yarn Commands

[YarnCommand ("StartGame")]
public void CallGameStart()
{
  Debug.Log("passing command from yarn to GameManager");
DemoManager dM = GetComponent<DemoManager>();
  dM.StartMenu();
}

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

  [YarnCommand("SetPortrait")]
  public void SetDialoguePortrait(string nameVar)
  {
    //Debug.Log("Fetching NPC Portrait. Target is "+ nameVar);
    Demo_InteractableNPC activeNPC;    
    string name;

    InMemoryVariableStorage varStor = FindObjectOfType<InMemoryVariableStorage>();
    varStor.TryGetValue(nameVar, out name);
    //Debug.Log("name found in MemVarStor = "+name);

    foreach (Demo_InteractableNPC n in activeNPCList)
    {
      if (name == n.myName)
      {
        //Debug.Log("Assigning portrait of "+n.myName);
        activeNPC = n;
        activeNPC.SetOutlineActive(true);
        activeNPC.SelectedOutlineOverride(true);
        LivePortrait.sprite = n.Icon;
      }
      else
      {
        //Debug.Log("NPC "+ n.name + "does not match name passed");
      }
    }    
  }

  [YarnCommand("StartMini")]
  public void callMinigame(string miniName)
  {
    DemoManager dM = GetComponent<DemoManager>();

    dM.StartMinigame(miniName);
  }

  

  //Internal Functions

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

  public void SceneTransitionNPC(int sceneRef)
  {
    foreach (Demo_InteractableNPC n in activeNPCList)
    {
      SceneBehaviourNPC sB = n.gameObject.GetComponent<SceneBehaviourNPC>();
      if (sB != null)
      {
        sB.ChangeScene(sceneRef);         
      }
    }    
  }

}
}