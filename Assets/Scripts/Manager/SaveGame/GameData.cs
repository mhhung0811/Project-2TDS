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

	public int currentHPPieces;
	public int currentManaPieces;
	public int currentPowerPieces;
	
	public int gunSlots;
	public List<GunType> unlockedGuns = new();
	public List<GunType> currentGuns = new();
	
	public int itemSlots;
	public List<ItemType> unlockedItems = new();
	public List<ItemType> currentItems = new();

	public GameData()
	{
		maxHealth = 6;
		maxMana = 1000;
		gunSlots = 2;
		unlockedGuns.Add(GunType.GlockPro);
		currentGuns.Add(GunType.GlockPro);
	}
}
