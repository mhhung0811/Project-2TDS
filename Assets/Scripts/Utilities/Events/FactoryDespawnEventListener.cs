using UnityEngine;
using UnityEngine.Events;

public class FactoryDespawnEventListener : MonoBehaviour
{
    public FactoryDespawnEvent Event;

    public UnityEvent<Flyweight> Response;
    
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Flyweight flyweight)
    {
        Response.Invoke(flyweight);
    }
}