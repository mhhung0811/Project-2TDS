using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int health;

	public GameData()
	{
		level = 1;
		health = 6;
	}
}
