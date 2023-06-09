using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GAD375.Prototyper;

namespace TrashIsland
{

    public class TISoundManager : MonoBehaviour
    {
        [SerializeField]
        protected ObjectSound soundObject;

        // Start is called before the first frame update
        void Start()
        {
            SoundInfoEvent.Register(HandleSoundEvent);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnDestroy()
        {
            SoundInfoEvent.Unregister(HandleSoundEvent);
        }

        public void Play(string soundname)
        {
            soundObject.PlaySound(soundname);
        }

        protected void HandleSoundEvent(SoundInfoEvent e)
        {
            if (e.command == SoundInfoEvent.SoundCommand.PLAY)
            {
                soundObject.PlaySound(e.soundname);
            }
            else
            {
                Debug.Log("Unsupported sound info command");
            }
        }
    }
}