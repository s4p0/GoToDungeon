using Assets.Scripts.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventSystem : MonoBehaviour
{
    private Dictionary<System.Type, List<System.Action<object>>> handlers = new Dictionary<System.Type, List<System.Action<object>>>();
    public static GlobalEventSystem Events;
    private void Awake()
    {
        if (Events = null)
            Events = this;
        else
            Destroy(Events);
    }

    public void Subscribe<T>(System.Action<T> listener)
    {
        if(!handlers.TryGetValue(typeof(T), out List<Action<object>> list))
        {
            list = new List<Action<object>>();
            handlers.Add(typeof(T), list);
        }
        list.Add(e => listener((T)e));
    }

    public void Unsubscribe<T>(System.Action<T> listener)
    {
        if (handlers.TryGetValue(typeof(T), out List<Action<object>> list))
        {
            list.Remove(e => listener((T)e));
        }
    }

    public void Publish(IGlobalEvent globalEvent)
    {
        if (handlers.TryGetValue(globalEvent.GetType(), out List<Action<object>> list))
            foreach (var item in list)
                item.Invoke(globalEvent);
    }

}
