using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD375.Prototyper
{
    [System.Serializable]
    public class NamedData<T>
    {
        public string name;
        public T data;

        public static bool FindByName(NamedData<T>[] list, string itemname, out T result)
        {
            NamedData<T> item;
            result = default(T);
            bool found = FindItemByName(list, itemname, out item);
            if (found)
            {
                result = item.data;
            }
            return found;
        }

        public static bool FindItemByName<U>(NamedData<T>[] list, string itemname, out U result) where U: NamedData<T>
        {
            bool found = false;
            result = null;
            foreach(U item in list)
            {
                if (item.name == itemname)
                {
                    result = item;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Debug.Log("Item named " + itemname + "of type " + typeof(T) + " could not be found");
            }
            return found;
        }
    }

    interface IObjectCommander
    {
        void RunCommand(string namedata);

        void OnCommandFinished();

        void OnDrawGizmosSelected();
    }
}
