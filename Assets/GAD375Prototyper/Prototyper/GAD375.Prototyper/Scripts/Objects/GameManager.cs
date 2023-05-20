using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD375.Prototyper
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector]
        public  Checkpoint currentCheckPoint;
        private static GameManager _instance;
        
        public static GameManager instance
        {
            get { return _instance; }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
            
    }
}