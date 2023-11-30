using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaulieFaceControls : MonoBehaviour
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
}
