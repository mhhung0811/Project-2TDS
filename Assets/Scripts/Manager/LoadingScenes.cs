using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScenes : MonoBehaviour
{
	public IEnumerator LoadSceneAsync(string sceneName)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
		operation.allowSceneActivation = false; // Chặn scene load ngay lập tức

		while (operation.progress < 0.9f) // Chờ scene load đến 90%
		{
			yield return null;
		}

		Debug.Log("Scene Loaded 90%, waiting for activation...");

		yield return new WaitForSeconds(1f); // Tuỳ chọn: Delay 1 giây trước khi vào scene

		operation.allowSceneActivation = true; // Kích hoạt scene sau khi load xong

		while (!operation.isDone) // Chờ đến khi scene thực sự được active
		{
			yield return null;
		}

		Debug.Log("Scene Fully Loaded and Activated!");
		GameManager.Instance.ResumeGame(); // Tiếp tục game sau khi scene hoàn toàn load xong
	}
}
