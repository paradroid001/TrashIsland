using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

namespace TrashIsland
{
    public class UIControlEvent : GameEvent<UIControlEvent>
    {
        public enum UIControlCommand
        {
            HIDE,
            SHOW,
            UPDATE,
            DESTROY
        }
        public UIControlCommand command;
        public string windowName;
    }

    public class TIUIManager : MonoSingleton<TIUIManager>
    {
        Dictionary<string, TIUIWindow> windows;

        protected override void InitSingleton()
        {
            base.InitSingleton();
            windows = new Dictionary<string, TIUIWindow>();
        }
        public bool RegisterWindow(TIUIWindow w)
        {
            bool retval = false;
            if (!windows.ContainsKey(w.WindowName))
            {
                windows.Add(w.WindowName, w);
                //Debug.Log($"Registered window {w.WindowName}");
                retval = true;
            }
            else
            {
                //Debug.LogError($"Duplicate window name {w.WindowName}. Not Registering");
            }
            return retval;
        }

        public bool UnregisterWindow(TIUIWindow w)
        {
            bool retval = false;
            if (windows.ContainsKey(w.WindowName))
            {
                //Debug.Log($"Unregistered a window {w.WindowName}");
                windows.Remove(w.WindowName);
                retval = true;
            }
            else
            {
                //Debug.LogError($"Can't unregister unregistered window name {w.WindowName}.");
            }
            return retval;
        }

        public void CommandWindow(string windowName, UIControlEvent.UIControlCommand command)
        {
            if (windows.ContainsKey(windowName))
            {
                TIUIWindow w = windows[windowName];
                switch (command)
                {
                    case UIControlEvent.UIControlCommand.SHOW:
                        {
                            w.Show();
                            break;
                        }
                    case UIControlEvent.UIControlCommand.HIDE:
                        {
                            w.Hide();
                            break;
                        }
                    case UIControlEvent.UIControlCommand.UPDATE:
                        {
                            w.Refresh();
                            break;
                        }
                    case UIControlEvent.UIControlCommand.DESTROY:
                        {
                            w.Destroy();
                            break;
                        }
                    default:
                        {
                            //do nothing/
                            break;
                        }
                }
            }
        }
    }
}