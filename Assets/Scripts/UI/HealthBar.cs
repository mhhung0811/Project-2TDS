﻿using System.Collections;
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
		UpdateUiHealthBar(0);
		PlayerHp.OnChanged += UpdateUiHealthBar;
	}

	public void UpdateUiHealthBar(int newValue)
	{
		// Debug.Log(newValue);
		int maxHp = PlayerMaxHp.CurrentValue;
		int hp = PlayerHp.CurrentValue;
		int totalHearts = maxHp / 2;

		// Destroy all children
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}

		// Create new hearts
		for (int i = 0; i < totalHearts; i++)
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
	
	private void OnDestroy()
	{
		PlayerHp.OnChanged -= UpdateUiHealthBar;
		PlayerMaxHp.OnChanged -= UpdateUiHealthBar;
	}
}
