using System.Collections.Generic;
using Props;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private Collider2D roomBound;
    [SerializeField] private Transform roomCenter;
    [SerializeField] private Transform enemyHolder;
    [SerializeField] private bool isBossRoom = false;
    [SerializeField] private List<RoomDoor> roomDoors;
    
    [Header("SO Events")]
    [SerializeField] private Collider2DEvent changeRoomBound;
    [SerializeField] private RoomControllerEvent changeRoomIndex;
    
    private List<IRoomProp> _roomProps = new();
    private bool isCompleted = false;

    public void Init(bool isClear)
    {
        isCompleted = isClear;
        
        enemyHolder.GetComponent<EnemySpawner>().SpawnEnemies();
        // Disable enemy holder at the start
        enemyHolder.gameObject.SetActive(false);

        foreach(Transform enemyHolder in enemyHolder)
        {
            _roomProps.Add(enemyHolder.GetComponent<IRoomProp>());
		}
    }

    public void Entry()
    {
        // Update A*
        PathRequestManager.Instance.UpdatePos(roomCenter.position);
        
        changeRoomBound.Raise(roomBound);
        changeRoomIndex.Raise(this);
        
        if (isCompleted) return;
        enemyHolder.gameObject.SetActive(true);
        
        foreach(var prop in _roomProps)
        {
            prop.OnRoomEntry();
        }
        
        foreach (var door in roomDoors)
        {
            door.IsClose = true;
        }
    }

    public void Exit()
    {
        enemyHolder.gameObject.SetActive(false);
    }

    public void Refresh()
    {
        foreach (var prop in _roomProps)
        {
            prop.OnRoomRefresh();
        }
    }
    
    public void CompleteRoom()
    {
        isCompleted = true;
        foreach (var door in roomDoors)
        {
            door.IsClose = false;
        }
    }
    
    public void AddRoomProp(IRoomProp prop)
    {
        _roomProps.Add(prop);
    }
}