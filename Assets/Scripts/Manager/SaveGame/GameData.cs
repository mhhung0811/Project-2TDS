using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int health;
	public int maxHealth;
	public int mana;
	public int maxMana;
	public Vector2 LastSpawn;

	public GameData()
	{
		health = 6;
		maxHealth = 6;
		mana = 1000;
		maxMana = 1000;
	}
}
