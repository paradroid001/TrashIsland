using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace GAD375.Prototyper
{

    public class BehaviourEnabler : MonoBehaviour
    {
        [System.Serializable]
        public class BehaviourInfo : NamedData<MonoBehaviour>
        {
        }

        public BehaviourInfo[] behaviours;

        [YarnCommand("disable")]
        public void DisableBehaviour(string behaviourname)
        {
            MonoBehaviour behave;
            if (BehaviourInfo.FindByName(behaviours, behaviourname, out behave))
            {
                behave.enabled = false;
            }
        }
        [YarnCommand("enable")]
        public void EnableBehaviour(string behaviourname)
        {
            MonoBehaviour behave;
            if (BehaviourInfo.FindByName(behaviours, behaviourname, out behave))
            {
                behave.enabled = true;
            }
        }

        
    }
}