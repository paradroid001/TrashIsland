using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{
    [System.Serializable]
    public class SoundInfoEvent : GameEvent<SoundInfoEvent>
    {
        public enum SoundCommand
        {
            PLAY,
            STOP
        }
        public string soundname;
        public SoundCommand command;
    }

    public class TISoundEvent : MonoBehaviour
    {
        public void EmitPlay(string soundname)
        {
            Emit(soundname, SoundInfoEvent.SoundCommand.PLAY);
        }
        public void EmitStop(string soundname)
        {
            Emit(soundname, SoundInfoEvent.SoundCommand.STOP);
        }
        public void Emit(string soundname, SoundInfoEvent.SoundCommand cmd)
        {
            SoundInfoEvent s = new SoundInfoEvent();
            s.soundname = soundname;
            s.command = cmd;
            s.Call();
        }
    }
}
