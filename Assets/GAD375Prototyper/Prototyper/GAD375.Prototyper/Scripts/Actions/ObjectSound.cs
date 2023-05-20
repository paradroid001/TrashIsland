using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace GAD375.Prototyper
{
    [RequireComponent(typeof(AudioSource))]
    public class ObjectSound : MonoBehaviour
    {
        [System.Serializable]
        public class SoundInfo : NamedData<AudioClip>
        {
        }
        public SoundInfo[] sounds;
        AudioSource audioSource;
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        [YarnCommand("sound")]
        public void PlaySound(string soundname)
        {
            AudioClip clip;
            if ( SoundInfo.FindByName(sounds, soundname, out clip))
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}
