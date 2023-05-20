using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Yarn.Unity;

namespace GAD375.Prototyper
{
    
    public class ObjectMover : MonoBehaviour
    {
        [System.Serializable]
        public class PositionInfo : NamedData<Transform>
        {
            public bool useExternalMover = false;
        }
        public float speed;
        public PositionInfo[] positions;
        public float distanceThreshold = 0.1f;

        [Header("Optional external mover")]
        public UnityEvent<Vector3> externalMover;
        Vector3 destination;
        bool moving = false;
        bool usingExternalMover = false;

        [YarnCommand("move")]
        public void MoveObject(string posname)
        {
            PositionInfo posinfo;
            if ( PositionInfo.FindItemByName(positions, posname, out posinfo) )
            {
                destination = posinfo.data.position; //transforms position
                moving = true;
                if (posinfo.useExternalMover)
                {
                    usingExternalMover = true;
                }
                else
                {
                    usingExternalMover = false;
                }
            }
        }

        [YarnCommand("stop")]
        public void StopObject()
        {
            moving = false;
            //Can't test this, because we don't know.
            //if (usingExternalMover)
            //{
                externalMover.Invoke(transform.position);
            //}
        }

        [YarnCommand("teleport")]
        public void TeleportObject(string posname)
        {
            //Make pos so we don't overwrite destination on bad lookups.
            Transform pos;
            if ( PositionInfo.FindByName(positions, posname, out pos) )
            {
                destination = pos.position;
                transform.position = destination;
            }
        }

        //teleports an object named 'objectname' in the world.
        [YarnCommand("teleportnamed")]
        public void TeleportObject(string objectname, string posname)
        {
            GameObject g = GameObject.Find(objectname);
            if (g == null)
            {
                Debug.LogError("Command teleportnamed can't teleport unknown object " + objectname);
                return; //no object existed
            }
            //Make pos so we don't overwrite destination on bad lookups.
            Transform pos;
            if ( PositionInfo.FindByName(positions, posname, out pos) )
            {
                destination = pos.position;
                g.transform.position = destination;
            }
        }
        

        //Object mover invoked with dynamic args (from say, collision) will move the collided object
        //To the FIRST position
        public void TeleportObject(GameObject mover, GameObject g)//, string posname = "")
        {
            string posname = "";
            if (posname == "")
            {
                if (positions != null && positions.Length > 0)
                    posname = positions[0].name;
                else
                    return;
            }
            //Make pos so we don't overwrite destination on bad lookups.
            Transform pos;
            if ( PositionInfo.FindByName(positions, posname, out pos) )
            {
                Debug.Log("Teleport found named position");
                destination = pos.position;
                g.transform.position = destination;
            }
        }



        /// Draw the position
        public void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.blue;
            if (positions == null)
                return;
            foreach (PositionInfo p in positions)
            {
                if (p.data != null)
                    Gizmos.DrawWireSphere(p.data.position, 0.2f);
            }
        }

        void Update()
        {
            if (moving)
            {
                Vector3 difference = transform.position - destination;
                if (difference.magnitude < distanceThreshold)
                {
                    //We have arrived
                    //TODO: a better way of doing this might be to have the agent
                    //give up if you arent getting any closer to your destination
                    OnMoveFinish();
                    moving = false;
                }
                else if (!usingExternalMover)
                {
                    Vector3 movement = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                    transform.position = movement;
                }
                else
                {
                    externalMover.Invoke(destination);
                    Debug.Log(name + " External move: " + destination);
                }
            }
        }

        public void OnMoveFinish()
        {
            Debug.Log("Reached Destination");
        }

    }
}