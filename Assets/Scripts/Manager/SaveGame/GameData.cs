using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int health;
    public int playerPos;

	public GameData(int level, int health, int playerPos)
	{
		this.level = level;
		this.health = health;
		this.playerPos = playerPos;
	}
}
