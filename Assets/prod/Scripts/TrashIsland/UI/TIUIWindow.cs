using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{
    public class TIUIWindow : MonoBehaviour, IUIWindow
    {
        [SerializeField]
        protected string windowName; //should be unique
        protected bool hidden = false;
        protected bool registered = false;

        public string WindowName
        {
            get { return windowName; }
        }

        public virtual void Hide()
        {
            hidden = true;
            ShowWindow(!hidden);
        }
        public virtual void Show()
        {
            hidden = false;
            ShowWindow(!hidden);
        }
        public virtual void Toggle()
        {
            if (hidden)
                Show();
            else
                Hide();
        }

        public virtual void Refresh()
        {
            //refresh window contents with updated values
        }
        public virtual void Destroy()
        {
            GameObject.Destroy(gameObject); //will call OnDestroy for us. 
        }

        //Override this method to use other ways of hiding/showing window.
        protected virtual void ShowWindow(bool shouldShow)
        {
            gameObject.SetActive(shouldShow);
        }

        void HandleWindowControlEvent(UIControlEvent e)
        {
            if (e.windowName != windowName)
                return;
            if (e.command == UIControlEvent.UIControlCommand.HIDE)
                Hide();
            else if (e.command == UIControlEvent.UIControlCommand.SHOW)
                Show();
            else if (e.command == UIControlEvent.UIControlCommand.DESTROY)
                Destroy();
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            registered = TIUIManager.Instance.RegisterWindow(this);
            if (registered)
            {
                UIControlEvent.Register(HandleWindowControlEvent);
            }
        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        protected virtual void OnDestroy()
        {
            if (registered)
            {
                TIUIManager.Instance.UnregisterWindow(this);
                UIControlEvent.Unregister(HandleWindowControlEvent);
            }
        }
    }
}