using System.Collections.Generic;
using UnityEngine;

namespace TrashIsland
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "TrashIsland/GameSettings", order = 1)]
    public class TIGameSettings : ScriptableObject
    {
        [Header("Frame Settings")]
        int maxRate = 9999;
        public int targetFrameRate = 60;
        [Header("Scene Settings")]
        public TISceneData[] sceneData;
        Dictionary<string, TISceneData> scenes;
        //float currentFrameTime;
        public void Init()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFrameRate;
            //currentFrameTime = Time.realtimeSinceStartup;
            scenes = BuildSceneDictionary();
        }


        //Scenes are non nullable.
        //To know if a scene is in the build, you need to find the asset path,
        //then find the build index using SceneUtility. If it is -1, it's not in the build.
        private Dictionary<string, TISceneData> BuildSceneDictionary()
        {
            Dictionary<string, TISceneData> retval = new Dictionary<string, TISceneData>();

            for (int i = 0; i < sceneData.Length; i++)
            {
                TISceneData sdata = sceneData[i];
                if (sdata.Validate())
                {
                    retval[sdata.sceneName] = sdata;
                }
                else
                {
                    Debug.LogError(sdata.GetErrorString());
                }
            }
            return retval;
        }

        public TISceneData GetSceneData(string sceneName)
        {
            if (scenes.ContainsKey(sceneName))
                return scenes[sceneName];
            else
                return null;
        }

        public TISceneData GetSceneDataFromBuildName(string buildName)
        {
            foreach (KeyValuePair<string, TISceneData> kvp in scenes)
            {
                if (kvp.Value.sceneBuildName == buildName)
                {
                    return kvp.Value;
                }
            }
            return null;
        }
    }
}