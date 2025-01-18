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

	public override void Awake()
	{
		base.Awake();
		Application.targetFrameRate = 60;

		foreach (IResetScene reset in listReset)
		{
			resetEvent.AddListener(reset.ResetScene);
		}
	}

	public void PauseGame()
	{
		isGamePaused = true;
		Time.timeScale = 1;
		SoundManager.Instance.PauseAllSounds();
	}

	public void ResumeGame()
	{
		isGamePaused = false;
		Time.timeScale = 1;
		SoundManager.Instance.ResumeAllSounds();
	}
}
