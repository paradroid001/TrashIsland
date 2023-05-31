using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

namespace TrashIsland
{
    //A modified Line View which emits an 'onRunline' event, which helps us know when dialogue starts.
    public class TIDialogueLineView : LineView
    {
        public UnityEvent onRunLine;
        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            onRunLine?.Invoke();
            base.RunLine(dialogueLine, onDialogueLineFinished);
        }
    }
}