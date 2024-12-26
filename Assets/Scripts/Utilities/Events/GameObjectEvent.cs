using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "GameObject Event", menuName = "Event/GameObject Event")]
public class GameObjectEvent : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GameObjectEventListener> eventListeners = new List<GameObjectEventListener>(); 
    
    public void Raise(GameObject gameObject)
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised(gameObject);
    }
    
    public void RegisterListener(GameObjectEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }
    
    public void UnregisterListener(GameObjectEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}