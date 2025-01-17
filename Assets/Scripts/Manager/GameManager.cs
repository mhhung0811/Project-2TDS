using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public bool isGamePaused = false;
	public bool isHoldButtonTab = false;

	public override void Awake()
	{
		base.Awake();
		Application.targetFrameRate = 60;
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
}
