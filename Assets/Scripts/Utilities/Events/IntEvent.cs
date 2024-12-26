using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Int Event", menuName = "Event/Int Event")]
public class IntEvent : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<IntEventListener> eventListeners = new List<IntEventListener>();
    
    public void Raise(int value)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(value);
    }
    
    public void RegisterListener(IntEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }
    
    public void UnregisterListener(IntEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}