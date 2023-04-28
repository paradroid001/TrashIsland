using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingPiece : MonoBehaviour
{
    public enum ClothingType{Hair, Shirt, Pants, Shoes, Accessory}
    public ClothingType typeOfClothes;
    public float yOffset;
    public float xOffset;
    public float zOffset;
    void Start()
    {
        transform.localPosition = new Vector3(xOffset, yOffset, zOffset);
    }
}
