using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFaceControls : MonoBehaviour
{
    public SpriteRenderer sprite;
    public List<Sprite> sprites;
    [Range(0, 2)]
    public int whichSprite;
    public void Start()
    {

        //sprite = GetComponent<SpriteRenderer>();
    }
    public void LateUpdate() 
    {
        if(sprite.sprite != sprites[whichSprite])
        {
            sprite.sprite = sprites[whichSprite];
        }
    }

    public void UpdateFace(string emotion)
    {
        switch(emotion)
        {
            case "Joy":
            whichSprite = 1;
            break;
            case "Sad":
            whichSprite = 2;
            break;
            case "":
            whichSprite = 3;
            break;
        }

        Apply();
    }
    public void ResetFace()
    {
        whichSprite = 0;
        Apply();
    }

    private void Apply()
    {
        sprite.sprite = sprites[whichSprite];
    }
}
