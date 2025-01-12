using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    public List<(EnemyType, Vector3)> enemySpawnPoints;

    public Wave()
    {
        enemySpawnPoints = new List<(EnemyType, Vector3)>();
    }
}

public class RoomController : MonoBehaviour
{
    private RoomStateMachine _roomStateMachine;
    private List<Wave> _waves = new List<Wave>();
    private List<Door> _doors = new List<Door>();
    private int waveIndex = 0;

    public bool isActiveRoom = false;
    
    public Transform enemyHolder;
    public Transform doorHolder;

    public VoidEnemyTypeVector3FuncProvider SpawnEnemyFunc;
    
    private void Awake()
    {
        _roomStateMachine = new RoomStateMachine(this);
        _roomStateMachine.Initialize(isActiveRoom);
        
        RetrieveEnemySpawnPoints();
        RetrieveDoors();
    }

    private void Start()
    {
        // Debug: Print wave information
        // for (int i = 0; i < _waves.Count; i++)
        // {
        //     Debug.Log($"Wave {i}:");
        //     foreach (Vector3 spawnPoint in _waves[i].enemySpawnPoints)
        //     {
        //         Debug.Log($"Spawn Point: {spawnPoint}");
        //     }
        // }
    }

    private void Update()
    {
        _roomStateMachine.FrameExecute();
    }
    
    private void FixedUpdate()
    {
        _roomStateMachine.PhysicsExecute();
    }
    
    private void RetrieveEnemySpawnPoints()
    {
        foreach (Transform waveTransform in enemyHolder)
        {
            Wave wave = new Wave();
            
            foreach (Transform spawnPointTransform in waveTransform)
            {
                wave.enemySpawnPoints.Add((spawnPointTransform.GetComponent<EnemyTag>().enemyType, spawnPointTransform.position));
            }
            
            _waves.Add(wave);
        }
    }
    
    private void RetrieveDoors()
    {
        foreach (Transform doorTransform in doorHolder)
        {
            _doors.Add(doorTransform.gameObject.GetComponent<Door>());
        }
    }


    #region Events

    public void OnRoomTriggered()
    {
        _roomStateMachine.TransitionTo(_roomStateMachine.triggeredState);
    }

    #endregion

    public void RoomTriggered()
    {
        // Room cleared
        if (waveIndex >= _waves.Count)
        {
            // open the door
            foreach (Door door in _doors)
            {
                door.IsClose = false;
            }
            
            _roomStateMachine.TransitionTo(_roomStateMachine.idleState);
            return;
        }
        
        // First entry
        if (waveIndex == 0)
        {
            // shut the door
            foreach (Door door in _doors)
            {
                door.IsClose = true;
                door.TurnOffInteractionZone();
            }
            
            // Move A*
            PathRequestManager.Instance.transform.position = transform.position;
        }
        
        // spawn enemies
        foreach ((EnemyType enemyType, Vector3 position) in _waves[waveIndex].enemySpawnPoints)
        {
            SpawnEnemyFunc.GetFunction()((enemyType, position));
        }
        
        Debug.Log("Spawn enemies");
        
        waveIndex++;
    }
}