using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashPaint : MonoBehaviour
{
    [SerializeField] 
    private Camera _camera;

    [SerializeField] 
    Texture2D _dirtMaskBase;
    [SerializeField]
    private Rect _sourceRect;

    [SerializeField] 
    private Texture2D _brush;

    [SerializeField] 
    private Material _material;
    [SerializeField]
    private Renderer _myRenderer;

    [SerializeField]
    private Color32[] pixelsInReadTexture;

    private Texture2D _templateDirtMask;

    [SerializeField]
    private bool isCalculating;
    [SerializeField]
    private int[] parts;
    [SerializeField]
    int skippedPixels = 10;

    [SerializeField]
    private float TransparencyTarget;
    [SerializeField]
    private float CurrentTransparency;
    [SerializeField]
    private float completionPercent;

    [SerializeField]
    private float PercentToWin;



    private void Start()
    {
        _material = gameObject.GetComponent<Renderer>().material;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        CreateTexture();    

         _myRenderer = GetComponent<Renderer>();
         _myRenderer.material = _material;

        if (GetComponent<MeshCollider>() == null && GetComponent<MeshFilter>() != null)
        {
            gameObject.AddComponent<MeshCollider>();
        }

        RefreshReadTexture(_dirtMaskBase, false);

        /*
        int x = Mathf.FloorToInt(_dirtMaskBase.x);
        int y = Mathf.FloorToInt(_dirtMaskLive.y);
        int width = Mathf.FloorToInt(_dirtMaskBase.width);
        int height= Mathf.FloorToInt(_dirtMaskBase.height);

        Color[] pix = _dirtMaskBase.GetPixels(x, y, width, height);
        _dirtMaskLive = new Texture2D(width, height);
        _dirtMaskLive.SetPixels(pix);
        _dirtMaskLive.Apply();
        */

        //RefreshReadTexture();
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
                completionPercent = GetPercent(TransparencyTarget, CurrentTransparency);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            RefreshReadTexture(_templateDirtMask, true);
        }

        if (completionPercent >= 50f && completionPercent >= PercentToWin)
        {
            Debug.Log("Win!");
        }


        
        
        
    }

    private float GetPercent(float maximum, float current)
    {
        float x = maximum - current; //value of amount we have removed
        float y = (x / maximum) * 100;
        Debug.Log(y +"% cleaned");
        return y;
    }

    private void CreateTexture()
    {
        _templateDirtMask = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _templateDirtMask.SetPixels(_dirtMaskBase.GetPixels());
        _templateDirtMask.Apply();

        _material.SetTexture("_MaskTexture", _templateDirtMask);
    }

    //WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();
    private void RefreshReadTexture(Texture2D texture, bool identifier)
    {
        //yield return frameEnd;

        RenderTexture.active = _myRenderer.material.mainTexture as RenderTexture;
        //_dirtMaskBase.ReadPixels(, 0, 0);  
        //RenderTexture.active = null;

        pixelsInReadTexture = texture.GetPixels32();

        if (!identifier)
        {
            TransparencyTarget = EstimateTransparenctPix(pixelsInReadTexture);
        }
        else
        {
            CurrentTransparency = EstimateTransparenctPix(pixelsInReadTexture);
        }
        
        
    }

    public static void GetRTPixels(RenderTexture rt)
    {
        //RenderTexture.active = _myRenderer.material.mainTexture as RenderTexture;
        //_dirtMaskBase.ReadPixels(, 0, 0);  
        //RenderTexture.active = null;
    }

    public static float EstimateTransparenctPix(Color32[] pxls)
    {
        int tested = 0;
        int tran = 0;
        int solid = 0;
        float solidF;

        for(int i = 0; i < pxls.Length; i++)	
        {
            tested++;
            if(pxls[i].a < 255)
            {
                solid++;
            }

            
        }
        
        tran = tested - solid;

        //Debug.Log("tested: "+tested);
        //Debug.Log("trans: "+tran);

        //Debug.Log("Solid = " + solid);
        solidF = solid;
        return solidF;
    }

    /*
    private IEnumerator CheckPixelsForTransparency(Color32 pixelsInArray)
    {
        isCalculating = true;
        
        int testedPixels = 0;

        for (int part=0; part<parts.Length; part++)
    }  
    */



}
