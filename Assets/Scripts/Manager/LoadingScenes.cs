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

		AsyncOperation operation = SceneManager.LoadSceneAsync("MapTest");

		while (!operation.isDone)
		{
			yield return null;
		}
	}
}