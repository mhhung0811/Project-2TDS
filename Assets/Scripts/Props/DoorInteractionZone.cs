using UnityEngine;

public class DoorInteractionZone : MonoBehaviour
{
    private Door _door;
    
    private void Awake()
    {
        _door = GetComponentInParent<Door>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _door.DoorTrigger();
        }
    }
}
