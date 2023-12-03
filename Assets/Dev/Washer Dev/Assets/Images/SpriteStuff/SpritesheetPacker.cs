using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpritesheetPacker", order = 1)]
public class SpritesheetPacker : ScriptableObject
{
    [SerializeField]
    private Vector2 spriteSize;
    [SerializeField]
    private Vector2 generatedSheetSize;
    [SerializeField]
    private List<Vector4> cellsToFill;

    [SerializeField]
    private int numberOfSprites;

    [SerializeField]
    private int cellSquared;
    private List<Texture2D> sprites;

    public bool hasRoot;

    private string parentPath;
    private string generatedSpritesheetPath;

    [SerializeField]
    private string spritesPath;
    private string processedPath;
    public void Generate()
    {
        GetFileArray(spritesPath, sprites);

        if (sprites != null)
        {
            numberOfSprites = sprites.Count;

            if (spriteSize == null)
            {
                Debug.Log("Setting Sprite Size");
                spriteSize.x = sprites[0].width;
                spriteSize.y = sprites[0].height;
            }

            cellSquared = Mathf.CeilToInt(Mathf.Sqrt(numberOfSprites));

            generatedSheetSize.x = (cellSquared*spriteSize.x);
            generatedSheetSize.y = (cellSquared*spriteSize.y);
            
            for (int i = 0; i<numberOfSprites; i++)
            {
                int index = i + 1;
                int myX = 0;
                int myY = 0;
                for (int cSq= 1; cSq<=cellSquared; cSq++)
                {
                    int rowRef = cSq*cellSquared;
                    if (index <= rowRef)
                    {
                        myY = cSq;

                        cSq--;
                        myX = index - (cSq*cellSquared);
                    }
                }
                

                //Vector4 myVal = Vector4.zero;
                Texture2D mySprite = sprites[i];


                Vector2 startPos = GenerateCoords(myX, myY);
                Vector2 endPos = new Vector2((startPos.x + spriteSize.x), (startPos.y + spriteSize.y));
                Vector4 myVal = new Vector4(startPos.x, startPos.y, endPos.x, endPos.y);


                cellsToFill.Add(myVal);
            }
        }

        
    } 
    private Vector2 GenerateCoords(int xPos, int yPos)
    {
        Vector2 v2 = new Vector2();
        int newX = xPos - 1; //Takes one off x to give 0 as starting pos for texture transplant
        v2.x = newX*spriteSize.x;

        int newY = cellSquared - yPos;
        v2.y = newY*spriteSize.y;


        return v2;
    }


    
    public void CreateFolder()
    {        
        Debug.Log(RootPath);
        if (RootPath != null)
        {
            hasRoot = true;

            string paPath = AssetDatabase.CreateFolder(RootPath, "Sprites");
            string geShPath = AssetDatabase.CreateFolder((RootPath+"/Sprites"), "Generated Spritesheets");
            string spPath = AssetDatabase.CreateFolder((RootPath+"/Sprites"), "Sprites To Pack");
            string prPath = AssetDatabase.CreateFolder((RootPath+"/Sprites"), "Processed Sprites");

            parentPath = AssetDatabase.GUIDToAssetPath(paPath);
            generatedSpritesheetPath = AssetDatabase.GUIDToAssetPath(geShPath);
            spritesPath = AssetDatabase.GUIDToAssetPath(spPath);
            processedPath = AssetDatabase.GUIDToAssetPath(prPath);

            Debug.Log(parentPath);
            Debug.Log(generatedSpritesheetPath);
            Debug.Log(spritesPath);
            Debug.Log(processedPath);

            /*
            CreateFile(parentPath);
            CreateFile(generatedSpritesheetPath);
            CreateFile(spritesPath);
            CreateFile(processedPath);
            */
            //ApplyChanges();
        }
        else
        {
            Debug.Log("Error: RootPath Generation Failed");
        }

        

    }


    private void GetFileArray(string p, List<Texture2D> l)
    {
        int index = 0;

        if (sprites != null)
        {
            sprites.Clear();
        }
        Debug.Log("Gathering files in " + p);
        //Object[] data = AssetDatabase.LoadAllAssetsAtPath(p);

        string[] data = AssetDatabase.FindAssets("t:Texture2D", new[] {p});
        foreach(string n in data)
        {
            string name = AssetDatabase.GUIDToAssetPath(n);
            var tBytes = File.ReadAllBytes(name);
            Texture2D tex = new Texture2D(1,1);
            tex.LoadImage(tBytes);

            tex.Apply();
            l.Add(tex);

            if (index == 0)
            {
                Debug.Log("Assigning Sprite Size");
                Vector2 v2 = new Vector2(tex.width, tex.height);
                spriteSize = v2;
            }

            index++;
        }
        
        /*
        foreach (Texture2D t in data)
        {
            l.Add(t);
            Debug.Log("Adding "+t.name);
        }
        */
    }
    private void CreateFile(string path)
    {
        if(!File.Exists(path+"/Tmp.txt"))
        {
            File.WriteAllText(path, "Hi");
        }
        else
        {
            Debug.Log("Error: Tmp.txt already exists at "+path);
        }

        File.AppendAllText(path, "don't read me, I'm shy");
    }
    static string RootPath
    {
        get
        {
            var g = AssetDatabase.FindAssets ( $"t:Script {nameof(SpritesheetPacker)}" );
            //return AssetDatabase.GUIDToAssetPath ( g [ 0 ] );
            string p = AssetDatabase.GUIDToAssetPath ( g [ 0 ] );
            return (p.Replace("/SpritesheetPacker.cs", ""));
            //return p;


        }
    }

    private void OnValidate()
    {
        EditorUtility.SetDirty(this);
        //Undo.RecordObject(this, "Applied Changes to Spritesheet Packer");
    }
}


