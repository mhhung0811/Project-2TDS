using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public IntVariable PlayerHp;
	public IntVariable PlayerMaxHp;
	public GameObject heartFull;
	public GameObject heartEmpty;
	public GameObject heartHalf;

	public void Start()
	{
		UpdateUiHealthBar();
	}

	public void UpdateUiHealthBar()
	{
		int maxHp = PlayerMaxHp.CurrentValue;
		int hp = 5;
		int totalHearts = maxHp / 2;

		Debug.Log("Updating health bar: " + hp + "/" + maxHp);

		// Destroy all children
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}

		// Create new hearts
		for(int i = 0; i < totalHearts; i++)
		{
			GameObject heart;
			if(hp >= 2)
			{

				heart = Instantiate(heartFull, transform);
				hp -= 2;
			}
			else if (hp == 1)
			{
				heart = Instantiate(heartHalf, transform);
				hp -= 1;
			}
			else
			{
				heart = Instantiate(heartEmpty, transform);
			}

			// Set the heart's position
			RectTransform rt = heart.GetComponent<RectTransform>();
			rt.anchoredPosition = new Vector2(70 * i + 60, 0);
		}
	}
}
