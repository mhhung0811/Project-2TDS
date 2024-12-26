using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "IntVector2Float Event", menuName = "Event/IntVector2Float Event")]
public class IntVector2FloatEvent : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<IntVector2FloatEventListener> eventListeners = new List<IntVector2FloatEventListener>();
    
    public void Raise(int intValue, Vector2 vector2Value, float floatValue)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(intValue, vector2Value, floatValue);
    }
    
    public void RegisterListener(IntVector2FloatEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }
    
    public void UnregisterListener(IntVector2FloatEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}