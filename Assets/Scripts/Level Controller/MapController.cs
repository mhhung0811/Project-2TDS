using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private IntVariable currentRoom;
    
    private List<LevelController> _rooms;
    
    private void Awake()
    {
        _rooms = new List<LevelController>(GetComponentsInChildren<LevelController>());
    }

    private void Start()
    {
        currentRoom.CurrentValue = SaveGameManager.Instance.gameData.lastRoom;
        
        // Load the current room
        _rooms[currentRoom.CurrentValue].Entry();
    }
    
    // Event listener
    public void ChangeRoom(LevelController room)
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