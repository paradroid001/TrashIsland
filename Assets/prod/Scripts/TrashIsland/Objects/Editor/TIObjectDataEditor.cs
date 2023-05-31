using UnityEngine;
using UnityEditor;
using System.Collections;

namespace TrashIsland
{

    [CustomEditor(typeof(TIObjectData))]
    public class TIObjectDataEditor : Editor
    {
        TIObjectData objectData;
        // Start is called before the first frame update
        void OnEnable()
        {
            objectData = target as TIObjectData;
        }

        //Here is the meat of the script
        public override void OnInspectorGUI()
        {
            //Draw whatever we already have in SO definition
            base.OnInspectorGUI();
            //Guard clause
            if (objectData.objectSprite == null)
                return;

            //Convert the weaponSprite (see SO script) to Texture
            Texture2D texture = AssetPreview.GetAssetPreview(objectData.objectSprite);
            //We crate empty space 80x80 (you may need to tweak it to scale better your sprite
            //This allows us to place the image JUST UNDER our default inspector
            GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
            //Draws the texture where we have defined our Label (empty space)
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }
    }
}
