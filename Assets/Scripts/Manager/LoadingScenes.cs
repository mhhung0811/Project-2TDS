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
		StartCoroutine(LoadSceneAsync(sceneName));
	}

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingScreen.SetActive(true);

		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

		while (!operation.isDone)
		{
			yield return null;
		}
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