using UnityEngine;
using UnityEngine.Events;

// Event system inspired from https://www.youtube.com/watch?v=raQ3iHhE_Kk
// Unite Austin 2017 - Game Architecture with Scriptable Objects

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Event that this GameObejct is listening to")]
    private GameEvent gameevent;

    [SerializeField]
    [Tooltip("Response that will be fired whenever the GameEvent is raised")]
    private UnityEvent response;

    private void OnEnable()
    {
        this.gameevent.RegisterListener(this);
    }

    private void OnDisable()
    {
        this.gameevent.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        this.response.Invoke();
    }
}
