using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{

    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        protected static T _instance;
        [SerializeField]
        protected bool dontDestroyOnLoad = true;

        protected virtual void InitSingleton()
        {

        }

        public static T Instance
        {
            get { return _instance; }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                if (dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(this);
                }
                InitSingleton();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void Start()
        {

        }
        protected virtual void Update()
        {

        }
    }
}