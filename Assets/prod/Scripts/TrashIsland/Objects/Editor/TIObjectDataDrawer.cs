using UnityEditor;
using UnityEngine;

namespace TrashIsland
{
    /*
    [CustomPropertyDrawer(typeof(TIObjectData))]
    public class TIObjectDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            //Draw properties here.
            // Calculate rects
            var nameRect = new Rect(position.x, position.y, position.width, position.height);
            var descriptionRect = new Rect(position.x, position.y + 30, position.width, position.height);
            var spriteRect = new Rect(position.x, position.y + 60, position.width, position.height);

            // Draw fields - 
            //  pass GUIContent.none if you want no labels
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"));
            EditorGUI.PropertyField(descriptionRect, property.FindPropertyRelative("description"));
            EditorGUI.PropertyField(spriteRect, property.FindPropertyRelative("sprite"));

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
    */
}