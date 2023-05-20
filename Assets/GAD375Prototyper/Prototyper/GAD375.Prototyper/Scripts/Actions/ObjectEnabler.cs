using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace GAD375.Prototyper
{
    public class ObjectEnabler : MonoBehaviour
    {
        [System.Serializable]
        public class ObjectInfo : NamedData<GameObject>
        {
        }

        public ObjectInfo[] objects;
        
        [YarnCommand("show")]
        public void EnableObject(string objname)
        {
            GameObject g;
            if (ObjectInfo.FindByName(objects, objname, out g))
            {
                g.SetActive(true);
            }
        }

        [YarnCommand("hide")]
        public void DisableObject(string objname)
        {
            GameObject g;
            if (ObjectInfo.FindByName(objects, objname, out g))
            {
                g.SetActive(false);
            }
        }
    }
}