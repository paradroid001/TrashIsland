using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashPaint : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Texture2D _dirtMaskBase;
    [SerializeField] private Texture2D _brush;

    [SerializeField] private Material _material;

    private Texture2D _templateDirtMask;

    private void Start()
    {
        _material = gameObject.GetComponent<Renderer>().material;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        CreateTexture();

        GetComponent<Renderer>().material = _material;

        if (GetComponent<MeshCollider>() == null && GetComponent<MeshFilter>() != null)
        {
            gameObject.AddComponent<MeshCollider>();
        }
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            //Debug.Log("Mouse Down");
            if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                //Debug.Log("hit");
                Vector2 textureCoord = hit.textureCoord;

                int pixelX = (int)(textureCoord.x * _templateDirtMask.width);
                int pixelY = (int)(textureCoord.y * _templateDirtMask.height);

                for (int x = 0; x < _brush.width; x++)
                {
                    for (int y = 0; y < _brush.width; y++)
                    {
                        

                        Color pixelDirt = _brush.GetPixel(x, y);
                        Color pixelDirtMask = _templateDirtMask.GetPixel(pixelX + x, pixelY + y);

                        _templateDirtMask.SetPixel(pixelX + x, pixelY + y, 
                            new Color(0, pixelDirtMask.g * pixelDirt.g, 0));

                    }
                }

                _templateDirtMask.Apply();
            }
        }
    }
    private void CreateTexture()
    {
        _templateDirtMask = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _templateDirtMask.SetPixels(_dirtMaskBase.GetPixels());
        _templateDirtMask.Apply();

        _material.SetTexture("_MaskTexture", _templateDirtMask);
    }

}
