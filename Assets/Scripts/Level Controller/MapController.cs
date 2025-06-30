using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> roomPrefabs;
    
    [SerializeField] private IntVariable currentRoom;
    
    private List<RoomController> _rooms = new();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        foreach (var r in roomPrefabs)
        {
            var room = Instantiate(r, transform).GetComponent<RoomController>();
            room.transform.SetParent(transform);
            room.Init();
            _rooms.Add(room);
        }
        
        currentRoom.CurrentValue = SaveGameManager.Instance.gameData.lastRoom;
        
        // Load the current room
        _rooms[currentRoom.CurrentValue].Entry();
    }
    
    // Event listener
    public void ChangeRoom(RoomController room)
    {
        currentRoom.CurrentValue = _rooms.IndexOf(room);
    }
    
    // Event listener
    public void RefreshRoom()
    {
        foreach (var room in _rooms)
        {
            room.Refresh();
        }
    }
}