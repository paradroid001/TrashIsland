using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorTexture;
    private Vector2 cursorHotspot;
    
    
    public void CursorSet(bool b)
    {
        if(b)
        {
            cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
            Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        }
        
    }


}
