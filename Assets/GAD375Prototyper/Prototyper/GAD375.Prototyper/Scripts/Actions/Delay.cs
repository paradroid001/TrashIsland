using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GAD375.Prototyper
{
    
    [System.Serializable]
    public class DelayedAction : UnityEvent
    {
    }

    [System.Serializable]
    public class DelayedActionInfo : NamedData<DelayedAction>
    {}

    [System.Serializable]
    public struct DelayInfo
    {
        public string actionName;
        public float delay;
    }


    public class Delay : MonoBehaviour
    {
        //public DelayedAction OnFinish = new DelayedAction();
        public DelayedActionInfo[] delayedActions;
        
        public void DelayCall(string actionstring)
        {
            string[] splittedParams = actionstring.Split(',');
            string action = splittedParams[0];
            string delaystr = splittedParams[1];
            float tempdelay;
            float delay = 0;
            if (float.TryParse(delaystr, out tempdelay) )
            {
                delay = tempdelay;
            }
            DelayedAction d;
            if (DelayedActionInfo.FindByName(delayedActions, action, out d))
            {
                StartCoroutine(DoDelay(d, delay));
            }
        }

        IEnumerator DoDelay(DelayedAction d, float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                d.Invoke();
            }
        }
    }

}