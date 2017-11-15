using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public enum EventType
{
    Equip,
    Collect,
    LoadUI
}

public class EventManager : MonoBehaviour
{
    static Dictionary<EventType, UnityEvent> eventDictionary;

    void Awake()
    {
        if (eventDictionary == null)
            eventDictionary = new Dictionary<EventType, UnityEvent>();
    }

    public static void StartListening(EventType eventType, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (eventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            eventDictionary.Add(eventType, thisEvent);
        }
    }

    public static void StopListening(EventType eventType, UnityAction listener)
    {    
        UnityEvent thisEvent = null;
        if (eventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(EventType eventType)
    {
        UnityEvent thisEvent = null;
        if (eventDictionary.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}