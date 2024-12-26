using UnityEngine;
using UnityEngine.Events;

public class IntVector2FloatEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public IntVector2FloatEvent Event;
    
    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<int, Vector2, float> Response;
    
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }
    
    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }
    
    public void OnEventRaised(int intValue, Vector2 vector2Value, float floatValue)
    {
        Response.Invoke(intValue, vector2Value, floatValue);
    }
}