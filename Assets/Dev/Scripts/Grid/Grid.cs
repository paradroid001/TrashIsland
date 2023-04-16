using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid
{
    private int width;
    private int height;
    private int[,] gridArray;
    public float x = -12.75f;
    public float y = -5;
    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;
        gridArray = new int[width, height];
        Debug.Log(width + " " + height);
        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                GameObject grid = new GameObject();
                grid.name = i + "," + j;
                grid.AddComponent<GridSquare>();
                SpriteRenderer sprite = grid.AddComponent<SpriteRenderer>();
                sprite.sprite = Resources.Load<Sprite>("Square");
                sprite.color = Color.red;
                grid.transform.position = new Vector2(x, y);
                x += 2.5f;
                if(x == 14.75)
                {
                    y += 2.55f;
                    x = -12.75f;
                }
                
                
            }
        }
    }

}
