using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{
    public class TISerialiser
    {
        public void LoadGame()
        {
            /*
            string filePath = Path.Combine(Application.streamingAssetsPath, saveFileToUse);
            if (File.Exists(filePath))
            {
                // Read the json from the file into a string
                _saveString = File.ReadAllText(filePath);
            }
            DruidSaveGameData savedata = JsonUtility.FromJson<DruidSaveGameData>(s);
            */
        }

        public void SaveGame()
        {
            /*
            DruidSaveGameData savedata = new DruidSaveGameData();
            //SOME OTHER GAME DATA HERE
            savedata.vars = variables.SerializeAllVariablesToJSON();
            savedata.dialogueNodes = nodeTracker.SerializeToJSON();
            string s = JsonUtility.ToJson(savedata, true); //prettyprint :)

            //Write the file
            string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
            File.WriteAllText(filePath, s);
            */
        }
    }
}