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
	private float timeInGame = 0;
	private bool startGame = false;

	public override void Awake()
	{
		base.Awake();
		Application.targetFrameRate = 60;

		timeInGame = 0;
		startGame = false;

		foreach (IResetScene reset in listReset)
		{
			resetEvent.AddListener(reset.ResetScene);
		}
	}

	public string GetTimeInGame()
	{
		int hours = (int)(timeInGame / 3600);
		int minutes = (int)((timeInGame % 3600) / 60);
		int seconds = (int)(timeInGame % 60);

		return hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
	}

	public void Update()
	{
		if (!isGamePaused && startGame)
		{
			timeInGame += Time.deltaTime;
		}
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
		timeInGame = 0;
		Debug.Log("Start Game");
	}

	public void ResetAllSO()
	{
		resetEvent.Invoke();
	}
}
