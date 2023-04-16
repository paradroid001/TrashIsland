using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clothing", menuName = "Inventory/Customise/Clothing")]
public class ClothingAsset : ScriptableObject
{
    public GameObject mesh;
    public ClothingPiece.ClothingType clothingType;
}
