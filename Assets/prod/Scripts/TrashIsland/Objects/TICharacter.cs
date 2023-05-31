using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
    /* Base class for TICharacters */
    public class TICharacter : MonoBehaviour
    {
        protected TICharacterMovement _movement;

        public TICharacterMovement movement{
            get {return _movement;}
        }

        virtual protected void Awake()
        {
            _movement = GetComponent<TICharacterMovement>();
        }

        // Start is called before the first frame update
        virtual protected void Start()
        {
            
        }

        // Update is called once per frame
        virtual protected void Update()
        {
            
        }
    }
}