using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
	public int maxHealth;
	public int maxMana;
	public Vector2 lastSpawn;
	public int lastRoom;

	public GameData()
	{
		maxHealth = 6;
		maxMana = 1000;
	}
}
