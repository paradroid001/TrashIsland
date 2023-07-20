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
        public Animator sceneTransitionAnimator;
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
//                            TIUIManager.Instance.CommandWindow("PauseMenu", UIControlEvent.UIControlCommand.HIDE);
//                            TIUIManager.Instance.CommandWindow("MainMenu", UIControlEvent.UIControlCommand.SHOW);
                            break;
                        }
                }
            }
            else
            {
                Debug.LogError($"Unknown scene loaded: {scene.name}");
            }
        }

        /**
        * Call this function to change scenes, giving the new scene name.
        * newSceneName must be one of the scenes in the build settings.
        * Optionally you can pass transition, which is an animator you have
        * set up somewhere in the sub structure of your GameManager. If this is
        * passed, SceneTransition will set the trigger (triggerName)
        * on the passed animator, presumably calling some transition animation you
        * have made. In the case that there is a need to wait a little while before
        * loading in the new scene (e.g. while the animation is playing), 
        * you can set waitTime to a positive value.
        * @param newSceneName must be one of the scenes in the build settings.
        * @param transition an optional Animator
        * @param triggerName the trigger to fire on the transition Animator
        * @param waitTime amount of seconds to wait before loading the new scene.
        */
        public virtual void SceneTransition(string newSceneName, Animator transition = null, string triggerNameOut = "SceneTransitionOut", string triggerNameIn = "SceneTransitionIn", float waitTime = 0.0f)
        {
            //Without transition
            if (transition == null && sceneTransitionAnimator == null)
            {
                ChangeScene(newSceneName);
            }
            else //with transition
            {
                Debug.Log("Animating scene transition");
                //Allow overriding with transition, else use sceneTransitionAnimator
                Animator transitionAnimator = transition;
                if (transitionAnimator == null)
                {
                    transitionAnimator = sceneTransitionAnimator;
                    waitTime = 1.0f;
                }

                StartCoroutine(TransitionToScene(newSceneName, sceneTransitionAnimator, triggerNameOut, triggerNameIn, waitTime));
            }
        }

        IEnumerator TransitionToScene(string newSceneName, Animator transition, string triggerNameOut, string triggerNameIn, float waitTime) //, OnSceneLoaded sceneLoadedFunction)
        {
            transition.SetTrigger(triggerNameOut);
            //Debug.Log("Transitioning");
            yield return new WaitForSecondsRealtime(waitTime);
            ChangeScene(newSceneName);
            transition.SetTrigger(triggerNameIn);
            //You might want to do 'on scene loaded' here? Because this is when its visible?
        }
    }
}