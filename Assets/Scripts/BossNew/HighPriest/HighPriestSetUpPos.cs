using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestSetUpPos : MonoBehaviour
{
	public List<Transform> posTele;
	public List<Vector2Variable> posTeleSO; 

	private void Start()
	{
		if(posTele.Count == 0 || posTeleSO.Count == 0)
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
}
