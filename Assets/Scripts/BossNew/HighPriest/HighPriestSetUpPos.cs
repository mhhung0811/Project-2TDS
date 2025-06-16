using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestSetUpPos : MonoBehaviour
{
	public Vector2Variable posCenter;
	public List<Transform> posTele;
	public List<Vector2Variable> posTeleSO;
	public GameObject cameraBoss;
	private void Start()
	{
		cameraBoss.SetActive(false);
		posCenter.CurrentValue = transform.position;

		if (posTele.Count == 0 || posTeleSO.Count == 0)
		{
			Debug.LogError("No teleport positions set up for High Priest.");
			return;
		}

		for (int i = 0; i < posTele.Count; i++)
		{
			posTeleSO[i].CurrentValue = posTele[i].position;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		foreach (Transform pos in posTele)
		{
			Gizmos.DrawWireSphere(pos.position, 0.5f);
		}
	}

	public void ActiveCamera()
	{
		cameraBoss.SetActive(true);
	}

	public void DeactiveCamera()
	{
		cameraBoss.SetActive(false);
	}
}
