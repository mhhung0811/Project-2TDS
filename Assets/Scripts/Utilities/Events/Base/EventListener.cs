using UnityEngine;
using UnityEngine.Events;

public class EventListener<T> : MonoBehaviour
{
    [TextArea(1, 10)]
    public string Description;
    
    [Tooltip("Event to register with.")]
    public Event<T> Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<T> Response;

    private void OnEnable()
    {
        Event?.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event?.UnregisterListener(this);
    }

    public void OnEventRaised(T value)
    {
        Response?.Invoke(value);
    }
}