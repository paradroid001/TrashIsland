using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

/*
namespace TrashIsland
{

    [CustomPropertyDrawer(typeof(TIObjectData))]
    public class TIObjectDataDrawerUIE : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create property container element.
            var container = new VisualElement();
            // Create property fields.
            var nameField = new PropertyField(property.FindPropertyRelative("name"));
            var descriptionField = new PropertyField(property.FindPropertyRelative("description"));
            var spriteField = new PropertyField(property.FindPropertyRelative("sprite"), "Object Sprite");

            // Add fields to the container.
            container.Add(nameField);
            container.Add(descriptionField);
            container.Add(spriteField);
            return container;
        }
        */
/*

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

    if (objectData == null)
        return;

    //Convert the weaponSprite (see SO script) to Texture
    Texture2D texture = AssetPreview.GetAssetPreview(objectData.sprite);
    //We crate empty space 80x80 (you may need to tweak it to scale better your sprite
    //This allows us to place the image JUST UNDER our default inspector
    GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
    //Draws the texture where we have defined our Label (empty space)
    GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
}
*/
/*
    }

}
*/

