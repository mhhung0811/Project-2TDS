using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUIBlockInputPlayer : MonoBehaviour
{
	public VoidEvent onOpenUI;
	private void OnEnable()
	{
		GameManager.Instance.isOpenUI = true;
		onOpenUI?.Raise(new Void());
	}

	private void OnDisable()
	{
		GameManager.Instance.isOpenUI = false;
	}
}
