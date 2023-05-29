using System;
using System.Collections.Generic;

namespace GameCore
{
    //thanks to: https://www.indiedb.com/members/damagefilter/blogs/event-and-unity
    /*
    All you have to do to use this is:
    1. Define an event class that you want:
    public class SomethingEvent : GameEvent<SomethingEvent>
    {
        public int somedata1;
        public string somedata2;
    }
    2. In some other entity, register for the event:
    public class Whatever
    {
        public Whatever()
        {
            SomethingEvent.Register(HandleSomething);
        }
        public void HandleSomething(SomethingEvent e)
        {
            //Do what you want..
            print(e.somedata1);
            print(e.somedata2);
        }
    }
    3. Trigger the event
    ...
        SomethingEvent somethingEvent = new SomethingEvent();
        somethingEvent.somedata1 = 2;
        somethingEvent.somedata2 = "data";
        somethingEvent.Call();
    ...
    */

    public interface IGameEvent { }
    // Defines the callback for events
    public delegate void GameEventCallback<in T>(T hook);

    internal interface IGameEventContainer
    {
        void Call(IGameEvent e);
        void Add(Delegate d);
        void Remove(Delegate d);
    }

    internal class GameEventContainer<T> : IGameEventContainer where T : IGameEvent
    {
        private GameEventCallback<T> events;
        public void Call(IGameEvent e)
        {
            if (events != null)
            {
                events((T)e);
            }
        }

        public void Add(Delegate d)
        {
            events += d as GameEventCallback<T>;
        }

        public void Remove(Delegate d)
        {
            events -= d as GameEventCallback<T>;
        }
    }

    public class GameEventDispatcher
    {
        private static GameEventDispatcher instance;

        public static GameEventDispatcher Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameEventDispatcher();
                }
                return instance;
            }
        }

        private Dictionary<Type, IGameEventContainer> registrants;

        private GameEventDispatcher()
        {
            registrants = new Dictionary<Type, IGameEventContainer>();
        }


        public void Register<T>(GameEventCallback<T> handler) where T : IGameEvent
        {
            var paramType = typeof(T);

            if (!registrants.ContainsKey(paramType))
            {
                registrants.Add(paramType, new GameEventContainer<T>());
            }
            var handles = registrants[paramType];
            handles.Add(handler);
        }

        public void Unregister<T>(GameEventCallback<T> handler) where T : IGameEvent
        {
            var paramType = typeof(T);

            if (!registrants.ContainsKey(paramType))
            {
                return;
            }
            var handlers = registrants[paramType];
            handlers.Remove(handler);
        }

        public void Call<T>(IGameEvent e) where T : IGameEvent
        {
            if (registrants.TryGetValue(typeof(T), out var d))
            {
                d.Call(e);
            }
        }
    }
    public abstract class GameEvent<T> : IGameEvent where T : IGameEvent
    {

        public void Call()
        {
            GameEventDispatcher.Instance.Call<T>(this);
        }

        public static void Register(GameEventCallback<T> handler)
        {
            GameEventDispatcher.Instance.Register(handler);
        }

        public static void Unregister(GameEventCallback<T> handler)
        {
            GameEventDispatcher.Instance.Unregister(handler);
        }
    }
}

