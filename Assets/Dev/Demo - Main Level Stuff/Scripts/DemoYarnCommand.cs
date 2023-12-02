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

    private Demo_InteractableNPC narrator;





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
  [YarnCommand("SetCharacter")]
    public Sprite PassSprite(string characterName)
    {
        Sprite spriteReturned = null;
        for (int i = 0; i < DemoManager.instance.dialoguePortraits.portraits.Count; i++)
        {
            if (DemoManager.instance.dialoguePortraits.portraits[i].charName == characterName)
            {
                spriteReturned = DemoManager.instance.dialoguePortraits.portraits[i].emotionSprites[0];
                LivePortrait.sprite = spriteReturned;
                Debug.Log("Name Found");
            }
            else if (i == DemoManager.instance.dialoguePortraits.portraits.Count && DemoManager.instance.dialoguePortraits.portraits[i].charName != characterName)
            {
                Debug.Log("Name Not Found");
                Debug.Log(characterName);
            }
        }
        return spriteReturned;
    }
    [YarnCommand("SetCharacterEmotion")]
    public Sprite PassSprite(string characterName, string emotionName)
    {
        Sprite spriteReturned = null;
        for(int i = 0; i < DemoManager.instance.dialoguePortraits.portraits.Count; i++)
        {
            if (DemoManager.instance.dialoguePortraits.portraits[i].charName == characterName)
            {
              Debug.Log("Name Found");
                if (DemoManager.instance.dialoguePortraits.portraits[i].emotionNames.Contains(emotionName))
                {
                  Debug.Log("Emotion Found");
                    int spriteNo = DemoManager.instance.dialoguePortraits.portraits[i].emotionNames.IndexOf(emotionName);
                    spriteReturned = DemoManager.instance.dialoguePortraits.portraits[i].emotionSprites[spriteNo];
                    LivePortrait.sprite = spriteReturned;
                }
                else if (i == DemoManager.instance.dialoguePortraits.portraits.Count && DemoManager.instance.dialoguePortraits.portraits[i].emotionNames[DemoManager.instance.dialoguePortraits.portraits[i].emotionNames.IndexOf(emotionName)] != emotionName)
                {
                    spriteReturned = DemoManager.instance.dialoguePortraits.portraits[i].emotionSprites[0];
                    LivePortrait.sprite = spriteReturned;
                    Debug.Log("Emotion Name Not Found");
                }
            }
            else if(i == DemoManager.instance.dialoguePortraits.portraits.Count && DemoManager.instance.dialoguePortraits.portraits[i].charName != characterName)
            {
                Debug.Log("Name Not Found");
            }
        }
        
        return spriteReturned;
    }

    [YarnCommand("SetNarrator")]
    public void SetDialogueNarrator(string name, string emotion)
    {
      //Debug.Log("Gathered Variables - name: "+name+", emotion: "+emotion);
    
      Demo_InteractableNPC activeNPC = null; 
      NPCFaceControls nFace = null;   
      
      
      string npcN = null;
      InMemoryVariableStorage varStor = FindObjectOfType<InMemoryVariableStorage>();
      varStor.TryGetValue(name, out npcN);
      Sprite spriteReturned = null;
      
      //Debug.Log("name gathered from storage = "+ npcN);

      foreach (Demo_InteractableNPC n in activeNPCList)
      {
        if (npcN == n.myName)
        {
          //Debug.Log("Assigning portrait of "+n.myName);
          
          activeNPC = n;
          nFace = n.GetComponent<NPCFaceControls>();

          if (narrator != activeNPC)//If we weren't already speaking
          {
            if (narrator != null)
            {
              narrator.CheckOutlineValid(); //Resets old narrating NPC's outline if we have swapped to a new narrator
              if(narrator.GetComponent<NPCFaceControls>()!= null) //If replaced narrator has face controls, reset face back to neutral 
              {
                narrator.GetComponent<NPCFaceControls>().ResetFace();
              }
            }         

            narrator = n; //Sets passed NPC as our narrator
            activeNPC.SetOutlineActive(true);
            activeNPC.SelectedOutlineOverride(true);
          }
        }
      }

      for(int i = 0; i < DemoManager.instance.dialoguePortraits.portraits.Count; i++)
      {
        //int npcNo;
        if (DemoManager.instance.dialoguePortraits.portraits[i].charName == narrator.myName)
        {
          Debug.Log("Name Found - " + narrator.myName);

          if (DemoManager.instance.dialoguePortraits.portraits[i].emotionNames.Contains(emotion))
          {
              Debug.Log("Emotion Found");
              int spriteNo = DemoManager.instance.dialoguePortraits.portraits[i].emotionNames.IndexOf(emotion);
              spriteReturned = DemoManager.instance.dialoguePortraits.portraits[i].emotionSprites[spriteNo];
              LivePortrait.sprite = spriteReturned;

              if (nFace != null)
              {
                nFace.UpdateFace(emotion);
              }
          }
          
          else if (i == DemoManager.instance.dialoguePortraits.portraits.Count && DemoManager.instance.dialoguePortraits.portraits[i].emotionNames[DemoManager.instance.dialoguePortraits.portraits[i].emotionNames.IndexOf(emotion)] != emotion)
          {
              spriteReturned = DemoManager.instance.dialoguePortraits.portraits[i].emotionSprites[0];
              LivePortrait.sprite = spriteReturned;
                      Debug.Log("Emotion Name Not Found");
                  }
              }
              else if(i == DemoManager.instance.dialoguePortraits.portraits.Count && DemoManager.instance.dialoguePortraits.portraits[i].charName != narrator.myName)
              {
                  Debug.Log("Name Not Found");
              }
          
        
          //LivePortrait.sprite = n.Icon;
        
      else
      {
        //Debug.Log("NPC "+ n.name + "does not match name passed");
      }
    } 
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