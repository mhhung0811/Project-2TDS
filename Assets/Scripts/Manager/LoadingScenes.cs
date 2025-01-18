using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadingScenes : MonoBehaviour
{
    public GameObject loadingScreen;
	void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
	{
		GameManager.Instance.resetEvent.Invoke();
		StartCoroutine(LoadSceneAsync(sceneName));
	}

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingScreen.SetActive(true);

		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

		while (!operation.isDone)
		{
			break;
		}

		Debug.Log("Scene Loaded");
		GameManager.Instance.ResumeGame();
		yield return null;
	}

    public void RestartScene()
    {
		string currentSceneName = SceneManager.GetActiveScene().name;
		GameManager.Instance.ResumeGame();
		SoundManager.Instance.StopAllSounds();
		StartCoroutine(LoadSceneAsync(currentSceneName));
	}

	public void Resume()
	{
		GameManager.Instance.ResumeGame();
	}
}