using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{
    [System.Serializable]
    public class InputCommandEvent : GameEvent<InputCommandEvent>
    {
        public enum InputCommand
        {
            ENABLE,
            DISABLE
        }
        public InputCommand command;
    }
    public class TIPlayerInputData
    {
        public float horizontalInput;
        public float verticalInput;
        public Vector2 pointerPosition;
        public float pointerHeldTime;
    }

    public class TIPlayerInputService : MonoBehaviour, IService, ITickedObject, IPlayerInput<TIPlayerInputData>
    {
        protected bool inputEnabled = true;
        protected TIPlayerInputData playerInputData;

        public virtual void InitService()
        {
            InputCommandEvent.Register(HandleInputCommandEvent);
        }
        public virtual void ShutdownService()
        {
            InputCommandEvent.Unregister(HandleInputCommandEvent);
        }

        public virtual void StartCollecting()
        {
            inputEnabled = true;
        }
        public virtual void StopCollecting()
        {
            inputEnabled = false;
        }

        public virtual void Tick(float dt)
        {
            if (!inputEnabled)
                return;
            CollectInput(dt);
        }

        public virtual void CollectInput(float dt)
        {

        }

        public virtual TIPlayerInputData GetState()
        {
            return playerInputData;
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            InitService();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            Tick(Time.deltaTime);
        }

        protected virtual void OnDestroy()
        {
            ShutdownService();
        }

        protected virtual void HandleInputCommandEvent(InputCommandEvent e)
        {
            if (e.command == InputCommandEvent.InputCommand.ENABLE)
            {
                StartCollecting();
            }
            else if (e.command == InputCommandEvent.InputCommand.DISABLE)
            {
                StopCollecting();
            }
        }
    }
}