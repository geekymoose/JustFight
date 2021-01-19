﻿using UnityEngine;
using UnityEngine.Events;

// Event system inspired from https://www.youtube.com/watch?v=raQ3iHhE_Kk
// Unite Austin 2017 - Game Architecture with Scriptable Objects

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Event that this GameObejct is listening to")]
    private GameEvent gameEvent;

    [SerializeField]
    [Tooltip("Response that will be fired whenever the GameEvent is raised")]
    private UnityEvent response;

    private void OnEnable()
    {
        this.gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        this.gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        this.response.Invoke();
    }
}
