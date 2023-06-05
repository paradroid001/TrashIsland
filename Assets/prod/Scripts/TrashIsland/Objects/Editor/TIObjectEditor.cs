using UnityEngine;
using UnityEditor;
using System.Collections;

namespace TrashIsland
{

    [CustomEditor(typeof(TIObject), true)] //editor for child classes
    public class TIObjectEditor : Editor
    {

        // We'll cache the editor here
        private Editor cachedEditor;

        public void OnEnable()
        {
            /* Resetting cachedEditor. This will ensure it is written to
            The next time OnInspectorGUI is called
            */
            cachedEditor = null;
        }

        public override void OnInspectorGUI()
        {
            // Grabbing the object this inspector is editing.
            TIObject editedMonobehaviour = (TIObject)target;

            /* Checking if we need to get our Editor. Calling Editor.CreateEditor() 
            if needed */
            //if (cachedEditor == null)
            //{
            //    cachedEditor =
            //        Editor.CreateEditor(editedMonobehaviour.objectData);
            //}

            /* We want to show the other variables in our Monobehaviour as well, 
            so we'll call the superclasses' OnInspectorGUI(). Note this could 
            also be accomplished by a call to DrawDefaultInspector() */
            base.OnInspectorGUI();

            //Drawing our ScriptableObjects inspector
            //cachedEditor.DrawDefaultInspector();

            //GUILayout.Label("---- Result Object Data ----");
            //if (cachedEditor != null)
            //    cachedEditor.OnInspectorGUI();
        }
    }
}