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
        var completedRooms = SaveGameManager.Instance.gameData.completedRooms;
        
        for (int i = 0; i < roomPrefabs.Count; i++)
        {
            var r = roomPrefabs[i];
            
            var room = Instantiate(r, transform).GetComponent<RoomController>();
            room.transform.SetParent(transform);
            room.Init(completedRooms.Contains(i));
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

    // Event listener
    public void ClearRoom()
    {
        var rooms = SaveGameManager.Instance.gameData.completedRooms;
        if (!rooms.Contains(currentRoom.CurrentValue))
        {
            _rooms[currentRoom.CurrentValue].CompleteRoom();
            rooms.Add(currentRoom.CurrentValue);
            SaveGameManager.Instance.SaveCompletedRoomsOnly(rooms);
            Debug.Log($"Room {currentRoom.CurrentValue} marked as cleared.");
        }
        else
        {
            Debug.Log($"Room {currentRoom.CurrentValue} is already cleared.");
        }
    }
}