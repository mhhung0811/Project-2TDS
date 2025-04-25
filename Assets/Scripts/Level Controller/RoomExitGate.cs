using UnityEngine;

public class RoomExitGate : MonoBehaviour
{
    [SerializeField] private RoomGate roomGate;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            roomGate.ExitGate(other.gameObject);
        }
    }
}