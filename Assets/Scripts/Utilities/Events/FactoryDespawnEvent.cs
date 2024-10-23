using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Factory Despawn Event", menuName = "Event/Factory Despawn Event")]
public class FactoryDespawnEvent : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<FactoryDespawnEventListener> eventListeners = new List<FactoryDespawnEventListener>();

    public void Raise(Flyweight flyweight)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(flyweight);
    }

    public void RegisterListener(FactoryDespawnEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(FactoryDespawnEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}