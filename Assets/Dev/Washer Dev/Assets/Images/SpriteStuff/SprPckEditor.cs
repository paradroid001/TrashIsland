using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpritesheetPacker))]

public class SprPckEditor : Editor
{
  public override void OnInspectorGUI()
  {
    

    base.OnInspectorGUI();

    SpritesheetPacker sprPck = (SpritesheetPacker)target;

    if (GUILayout.Button("Generate"))
    {
        sprPck.Generate();
    }

    if (sprPck.hasRoot == false)
    {
        if (GUILayout.Button("Create Root Folders") && !sprPck.hasRoot)
        {
            sprPck.CreateFolder();
            
        }
    }
    
  }
}
