using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GAD.Utils;

namespace GAD375.Prototyper
{
    public class Checkpoint : MonoBehaviour
    {
        [Header("Objects with this tag can collide")]
        [TagSelector]
        public string objectTag;
        Vector3 pos;
        Quaternion rot;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void SetRespawn(GameObject obj1, GameObject obj2)
        {
            GameObject player = obj2;
            pos = player.transform.position;
            rot = player.transform.rotation;
            GameManager.instance.currentCheckPoint = this;
        }

        /*
        void OnTriggerEnter(Collider col)
        {
            if (col.tag == objectTag)
            {
                GameObject player = col.gameObject;
                pos = player.transform.position;
                rot = player.transform.rotation;
                GameManager.instance.currentCheckPoint = this;
            }       
        }
        */

        public void Respawn(GameObject player)
        {
            player.transform.position = pos;
            player.transform.rotation = rot;
        }
    }

}