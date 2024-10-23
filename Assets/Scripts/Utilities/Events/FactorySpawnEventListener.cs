using UnityEngine;
using UnityEngine.Events;

public class FactorySpawnEventListener : MonoBehaviour
{
    public FactorySpawnEvent Event;
    
    public UnityEvent<FlyweightType, Vector2, float> Response;
    
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(FlyweightType type, Vector2 position, float rotation)
    {
        Response.Invoke(type, position, rotation);
    }
}