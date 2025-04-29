using UnityEngine;

public class RoomEnemy : MonoBehaviour, IRoomProp
{
    [SerializeField] private Enemy _enemy;

    public void OnRoomEntry()
    {
        if (_enemy.gameObject.activeSelf)
        {
            _enemy.CurrentHealth = _enemy.MaxHealth;
        }
    }

    public void OnRoomRefresh()
    {
        _enemy.Revive();
    }
}