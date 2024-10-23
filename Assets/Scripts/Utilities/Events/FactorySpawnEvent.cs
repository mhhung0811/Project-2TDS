using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Factory Spawn Event", menuName = "Event/Factory Spawn Event")]
public class FactorySpawnEvent : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<FactorySpawnEventListener> eventListeners = new List<FactorySpawnEventListener>();

    public void Raise(FlyweightType type, Vector2 position, float rotation)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(type, position, rotation);
    }

    public void RegisterListener(FactorySpawnEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(FactorySpawnEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    } 
}