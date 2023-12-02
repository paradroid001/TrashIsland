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
        if(emotion == "Joy")
        {
            whichSprite = 1;
        }

        else if(emotion == "Sad")
        {
            whichSprite = 2;
        }

        else
        {
            whichSprite = 0;
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
