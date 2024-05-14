using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace TrashIsland
{
public class NodeOverriderSPARC : MonoBehaviour
{
    enum OverrideBranch {None, Aluminium, Cardboard, Glass, Plastic};
    [SerializeField]
    OverrideBranch overrideBranch;

    [SerializeField]
    private int overrideProgressionRef;

    [SerializeField]
    private string oNodeName;

    private Demo_InteractableNPC npcScript;



    // Start is called before the first frame update
    void Start()
    {
        npcScript = gameObject.GetComponent<Demo_InteractableNPC>();
        overrideBranch = OverrideBranch.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [YarnCommand("ActivateOverride")]
    public void ActivateOverride(int materialRef)
    {
        if (materialRef == 1) // Aluminium
        {
            overrideBranch = OverrideBranch.Aluminium;
            overrideProgressionRef = 1;
        }
        if (materialRef == 2) // Cardboard
        {
            overrideBranch = OverrideBranch.Cardboard;
            overrideProgressionRef = 1;
        }
        if (materialRef == 3) // Glass
        {
            overrideBranch = OverrideBranch.Glass;
            overrideProgressionRef = 1;
        }
        if (materialRef == 4) // Plastic
        {
            overrideBranch = OverrideBranch.Plastic;
            overrideProgressionRef = 1;
        }

    }

    [YarnCommand("DeactivateOverride")]
    public void DeactivateOverride()
    {
        overrideBranch = OverrideBranch.None;
        overrideProgressionRef = 0;
        oNodeName = "SPARC_Directory";
        SetNode(oNodeName);
    }

    public void UpdateProgression()
    {
        overrideProgressionRef++;

        if (overrideBranch == OverrideBranch.Aluminium)
        {
            if(overrideProgressionRef == 2)
            {
                oNodeName = "AluminiumExplain2";
                SetNode(oNodeName);
            }
            if(overrideProgressionRef == 3)
            {
                oNodeName = "AluminiumExplain3";
                SetNode(oNodeName);
            }
            if(overrideProgressionRef == 4)
            {
                oNodeName = "AluminiumExplain4";
                SetNode(oNodeName);
            }
            else
            {
                Debug.Log("ERROR: Progression Ref Doesn't Match Range of Aluminium");
            }
        }
        if (overrideBranch == OverrideBranch.Cardboard)
        {
            if(overrideProgressionRef == 2)
            {
                oNodeName = "CardboardExplain2";
                SetNode(oNodeName);
            }
            if(overrideProgressionRef == 3)
            {
                oNodeName = "CardboardExplain3";
                SetNode(oNodeName);
            }
            if(overrideProgressionRef == 4)
            {
                oNodeName = "CardboardExplain4";
                SetNode(oNodeName);
            }
            else
            {
                Debug.Log("ERROR: Progression Ref Doesn't Match Range of Cardboard");
            }
        }
        
        if (overrideBranch == OverrideBranch.Glass)
        {
            if(overrideProgressionRef == 2)
            {
                oNodeName = "GlassExplain2";
                SetNode(oNodeName);
            }
            if(overrideProgressionRef == 3)
            {
                oNodeName = "GlassExplain3";
                SetNode(oNodeName);
            }
            if(overrideProgressionRef == 4)
            {
                oNodeName = "GlassExplain4";
                SetNode(oNodeName);
            }
            else
            {
                Debug.Log("ERROR: Progression Ref Doesn't Match Range of Glass");
            }
        }
        if (overrideBranch == OverrideBranch.Plastic)
        {
            if(overrideProgressionRef == 2)
            {
                oNodeName = "PlasticExplain2";
                SetNode(oNodeName);
            }
            if(overrideProgressionRef == 3)
            {
                oNodeName = "PlasticExplain3";
                SetNode(oNodeName);
            }
            if(overrideProgressionRef == 4)
            {
                oNodeName = "PlasticExplain4";
                SetNode(oNodeName);
            }
            else
            {
                Debug.Log("ERROR: Progression Ref Doesn't Match Range of Plastic");
            }
        }
    }

    void SetNode(string node)
    {
         npcScript.startNode = node;
    }
}
}
