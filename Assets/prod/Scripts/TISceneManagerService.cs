using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameCore;

namespace TrashIsland
{

    [System.Serializable]
    public class TISceneData
    {
        public string sceneName;
        //public SceneAsset sceneAsset;
        public string sceneBuildName;
        private Scene scene;
        private string errorString = "";
        private int buildIndex = -1;

        public Scene GetScene()
        {
            return scene;
        }
        public string GetErrorString()
        {
            return errorString;
        }
        public int GetBuildIndex()
        {
            return buildIndex;
        }

        public bool Validate()
        {
            bool retval = false;

            //No point getting 'scene': GetSceneByName only searches loaded scenes
            scene = SceneManager.GetSceneByName(sceneBuildName);
            string path = sceneBuildName;
            if (path == null)
            {
                errorString = $"Asset path could not be found for {sceneBuildName}.";
            }
            else
            {
                buildIndex = SceneUtility.GetBuildIndexByScenePath(path);
                if (buildIndex == -1)
                {
                    errorString = $"Scene [{sceneName}]: {scene.name} has not been added to the build.";
                }
                else
                {
                    retval = true;
                }
            }
            return retval;
        }
    }

    [System.Serializable]
    public class TISceneManagerService : MonoBehaviour, IService
    {
        TIGameSettings settings;

        Scene previousScene;
        Scene currentScene;
        Scene newScene;

        private bool inited = false;

        public void InitService(TIGameSettings s)
        {
            settings = s;
            InitService();
        }

        public void InitService()
        {
            if (!inited)
                SceneManager.sceneLoaded += OnLoadScene;
        }
        public void ShutdownService()
        {
            if (inited)
                SceneManager.sceneLoaded -= OnLoadScene;
        }

        void OnDestroy()
        {
            ShutdownService();
        }


        public void ChangeScene(string sceneName)
        {
            TISceneData sdata = settings.GetSceneData(sceneName);
            if (sdata != null)
            {
                SceneManager.LoadScene(sdata.sceneBuildName);
            }
            else
            {
                Debug.Log($"Couldn't find scene matching {sceneName}");
            }
        }

        void OnLoadScene(Scene scene, LoadSceneMode mode)
        {
            Debug.Log($"scene loaded: {scene.name}");
            TISceneData sceneData = settings.GetSceneDataFromBuildName(scene.name);
            if (sceneData != null)
            {
                switch (sceneData.sceneName)
                {
                    case "Game":
                        {
                            Debug.Log("Game scene loaded");
                            TIUIManager.Instance.CommandWindow("PauseMenu", UIControlEvent.UIControlCommand.HIDE);
                            TIUIManager.Instance.CommandWindow("MainMenu", UIControlEvent.UIControlCommand.HIDE);
                            break;
                        }
                    case "Menu":
                        {
                            Debug.Log("Menu scene loaded");
                            TIUIManager.Instance.CommandWindow("PauseMenu", UIControlEvent.UIControlCommand.HIDE);
                            TIUIManager.Instance.CommandWindow("MainMenu", UIControlEvent.UIControlCommand.SHOW);
                            break;
                        }
                }
            }
            else
            {
                Debug.LogError($"Unknown scene loaded: {scene.name}");
            }
        }
    }
}