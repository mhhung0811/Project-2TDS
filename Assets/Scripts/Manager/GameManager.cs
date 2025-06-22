using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
	public bool isGamePaused = false;
	public bool isHoldButtonTab = false;
	public List<ScriptableObject> listReset;
	public UnityEvent resetEvent;
	public LoadingScenes loadingScenes;
	public bool isOpenUI;

	public override void Awake()
	{
		base.Awake();
		Application.targetFrameRate = 60;


		foreach (IResetScene reset in listReset)
		{
			resetEvent.AddListener(reset.ResetScene);
		}
	}

	public void Start()
	{
		loadingScenes = GetComponent<LoadingScenes>();
		isOpenUI = false;
	}

	public void PauseGame()
	{
		isGamePaused = true;
		Time.timeScale = 0;
		SoundManager.Instance.PauseAllSounds();
	}

	public void ResumeGame()
	{
		isGamePaused = false;
		Time.timeScale = 1;
		SoundManager.Instance.ResumeAllSounds();
	}

	public void PlayNewGame()
	{
		// Override save new game
		GameData gameData = new GameData();
		SaveGameManager.Instance.SaveGame(gameData);
		// Load Data
		SaveGameManager.Instance.LoadGame();
		// Load Scene
		StartCoroutine(loadingScenes.LoadSceneAsync("Main"));
	}

	public void PlayContinueGame()
	{
		// Load data
		SaveGameManager.Instance.LoadGame();
		//
		StartCoroutine(loadingScenes.LoadSceneAsync("Main"));
	}

	public void ResetAllSO()
	{
		resetEvent.Invoke();
	}
}
