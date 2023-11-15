using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using GameCore;

namespace TrashIsland
{

    public class TIGameManager : MonoSingleton<TIGameManager>
    {
        public TIGameSettings settings;
        [SerializeField]
        TIUIManager uiManager;
        TISceneManagerService sceneManager;
        DialogueRunner dialogueRunner;
        TIDialogueLineView lineView;

        protected override void InitSingleton()
        {
            base.InitSingleton();
            //Init the game settings
            if (settings != null)
            {
                settings.Init();
            }
            sceneManager = GetComponent<TISceneManagerService>();
            sceneManager?.InitService(settings);

            dialogueRunner = FindObjectOfType<DialogueRunner>();
            lineView = FindObjectOfType<TIDialogueLineView>();

            lineView?.onRunLine.AddListener(OnDialogueStart);
            dialogueRunner?.onDialogueComplete.AddListener(OnDialogueEnd);
        }

        protected override void Start()
        {
            Debug.Log("GAME MANAGER START");
            sceneManager?.ChangeScene("Menu");
            //uiManager.CommandWindow("PauseMenu", UIControlEvent.UIControlCommand.HIDE);
            //uiManager.CommandWindow("Inventory", UIControlEvent.UIControlCommand.HIDE);
            //uiManager.CommandWindow("Inventory", UIControlEvent.UIControlCommand.DESTROY);
            //Invoke("ShowInventory", 3);
        }

        /*
        void ShowInventory()
        {
            uiManager.CommandWindow("Inventory", UIControlEvent.UIControlCommand.SHOW);
        }
        */

        public void OnDialogueStart()
        {
            //Debug.Log("DIalogyue start!");
            InputCommandEvent e = new()
            {
                command = InputCommandEvent.InputCommand.DISABLE
            };
            e.Call();
        }
        public void OnDialogueEnd()
        {
            Debug.Log("Dialogue END");
            InputCommandEvent e = new()
            {
                command = InputCommandEvent.InputCommand.ENABLE
            };
            e.Call();
        }

        public void OnStartGame()
        {
            Debug.Log("Start game pressed");
            sceneManager.SceneTransition("Game");
        }
        public void OnLoadGame()
        {
            Debug.Log("Load game not yet implemented");
        }

        public void OnBackToTitle()
        {
            UnPause();
            sceneManager.SceneTransition("Menu");
        }

        public void OnPauseGame()
        {
            Pause();
            uiManager.CommandWindow("PauseMenu", UIControlEvent.UIControlCommand.SHOW);
        }
        public void OnUnPauseGame()
        {
            UnPause();
            Debug.Log("Game Unpaused");
        }

        void Pause()
        {
            Debug.Log("Game Paused");
            Time.timeScale = 0.0f;
            InputCommandEvent e = new()
            {
                command = InputCommandEvent.InputCommand.DISABLE
            };
            e.Call();

        }
        void UnPause()
        {
            Debug.Log("Game UnPaused");
            Time.timeScale = 1.0f;
            InputCommandEvent e = new()
            {
                command = InputCommandEvent.InputCommand.ENABLE
            };
            e.Call();
        }
    }
}