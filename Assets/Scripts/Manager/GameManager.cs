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
	private bool startGame = false;
	private LoadingScenes loadingScenes;

	public override void Awake()
	{
		base.Awake();
		Application.targetFrameRate = 60;

		startGame = false;

		foreach (IResetScene reset in listReset)
		{
			resetEvent.AddListener(reset.ResetScene);
		}
	}

	public void Start()
	{
		loadingScenes = new LoadingScenes();
	}

	public void Update()
	{

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

	public void PlayGame() {
		startGame = true;
		Debug.Log("Start Game");
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
