using System;
using System.Collections;
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
    private EnemyTypeEventListener _enemyDownEventListener;
    private List<Wave> _waves = new List<Wave>();
    private List<Door> _doors = new List<Door>();
    private int waveIndex = 0;
    private int enemyIndex = 0;
    private Coroutine _delayedRoomTriggeredEndCoroutine;

    public bool isActiveRoom = false;
    public Transform roomCenter;
    
    public Transform enemyHolder;
    public Transform doorHolder;

    public GameObject miniMap;

    public RoomStateMachine roomStateMachine { get; private set; }
    public GameObjectEnemyTypeVector3FuncProvider SpawnEnemyFunc;

    public VoidEvent OnRoomTrigger;
    
    private void Awake()
    {
        RetrieveEnemySpawnPoints();
        RetrieveDoors();
        
        _enemyDownEventListener = GetComponent<EnemyTypeEventListener>();
        _enemyDownEventListener.enabled = false;
        roomStateMachine = new RoomStateMachine(this);
        roomStateMachine.Initialize(isActiveRoom);
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
        roomStateMachine.FrameExecute();
    }
    
    private void FixedUpdate()
    {
        roomStateMachine.PhysicsExecute();
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

    public void OnEnemyDown(EnemyType enemyType)
    {
        // wave index here have to minus 1 as OnEnemyDown is called in-wave where wave setup have already called
        
        enemyIndex++;
        Debug.Log(_waves[waveIndex - 2 >= 0 ? waveIndex - 2 : 0].enemySpawnPoints.Count);
        Debug.Log($"Wave index: {waveIndex}");
        Debug.Log($"Total waves: {_waves.Count}");
        
        // Check all enemies died
        if (waveIndex > 0 && enemyIndex < _waves[waveIndex - 2 >= 0 ? waveIndex - 2 : 0].enemySpawnPoints.Count) return;

        // Last wave
        if (waveIndex > _waves.Count)
        {
            // Debug.Log("Last wave enemy down");
            DelayTriggerdStateToState(roomStateMachine.idleState);
        }
        // Normal wave
        else
        {
            // Debug.Log("Normal wave enemy down");
            DelayTriggerdStateToState(roomStateMachine.activeState);
        }

        enemyIndex = 0;
    }

    #endregion

    #region State Call

    public void RoomSetUp()
    {
        waveIndex++;
        
        // Set up for first entry
        if (waveIndex == 1)
        {
            // Open all doors
            foreach (Door door in _doors)
            {
                door.IsClose = true;
                door.TurnOnInteractionZone();
            }
        }
    }
    
    public void RoomTriggered()
    {
        // First entry
        if (waveIndex == 1)
        {
			//Call the event
			if (OnRoomTrigger != null)
			{
				Debug.Log("------Room Triggered Boss");
				Void @void = new Void();
				OnRoomTrigger.Raise(@void);
			}
			// Active the enemy down event listener
			_enemyDownEventListener.enabled = true;
			//Open Minimap
            if(miniMap != null)
            {
			    miniMap.SetActive(true);
            }
			// Close all doors
			foreach (Door door in _doors)
            {
                door.IsClose = true;
                door.TurnOffInteractionZone();
            }
			// Play music
			SoundManager.Instance.PlayMusic();

			// Move A*
			PathRequestManager.Instance.UpdatePos(roomCenter.position);
        }

        Debug.Log(waveIndex);
        Debug.Log(_waves.Count);
        // Last wave
        if (waveIndex > _waves.Count) 
        { 
            SoundManager.Instance.StopMusic();
            return;
		};
        
        // spawn enemies
        if (waveIndex > 0)
        {
            foreach ((EnemyType enemyType, Vector3 position) in _waves[waveIndex - 1].enemySpawnPoints)
            {
                SpawnEnemyFunc.GetFunction()((enemyType, position));
            }
        }

		// Debug.Log("Spawn enemies");
	}

	public void RoomDeactivated()
    {
        // Deactive the enemy down event listener
        _enemyDownEventListener.enabled = false;
		// Open all doors
		foreach (Door door in _doors)
        {
            door.IsClose = false;
            door.TurnOffInteractionZone();
        }
    }

    #endregion
    
    public void DelayTriggerdStateToState(IState state, float delay = 0.1f)
    {
        roomStateMachine.TransitionTo(roomStateMachine.triggeredState);
            
        // Prevent coroutine call too many times
        if (_delayedRoomTriggeredEndCoroutine != null)
        {
            StopCoroutine(_delayedRoomTriggeredEndCoroutine);
        }
        _delayedRoomTriggeredEndCoroutine = StartCoroutine(DelayedToState(state, delay));
    }
    
    private IEnumerator DelayedToState(IState state, float delay)
    {
        yield return new WaitForSeconds(delay);
        roomStateMachine.TransitionTo(state);
    }
}