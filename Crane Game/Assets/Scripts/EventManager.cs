using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, Action> eventDictionary;

    private static EventManager _eventManager;

    public static EventManager Instance
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!_eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    _eventManager.Init();
                }
            }

            return _eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action>();
        }
    }

    public static void StartListening(string eventName, Action listener)
    {

        Action thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
        }
        else
        {
            thisEvent += listener;
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action listener)
    {
        if (_eventManager == null) return;
        Action thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
        }
    }

    public static void TriggerEvent(string eventName)
    {
        Action thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
