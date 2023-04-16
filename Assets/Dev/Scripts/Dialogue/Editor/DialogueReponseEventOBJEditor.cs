using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ResponseEventOBJ))]

public class ResponseEventsOBJEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ResponseEventOBJ responseEvents = (ResponseEventOBJ)target;

        if (GUILayout.Button("Refresh"))
        {
            responseEvents.OnValidate();
        }
    }
}