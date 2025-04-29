using UnityEngine;

public class OpenUIBlockInputPlayer : MonoBehaviour
{
	private void OnEnable()
	{
		GameManager.Instance.isOpenUI = true;
	}

	private void OnDisable()
	{
		GameManager.Instance.isOpenUI = false;
	}
}
