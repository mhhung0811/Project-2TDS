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
	public int currentGunIndex;
	public List<GunType> unlockedGuns = new();
	
	public int itemSlots;
	public int currentItemIndex;
	public List<ItemType> unlockedItems = new();

	public GameData()
	{
		maxHealth = 6;
		maxMana = 1000;
		gunSlots = 2;
		currentGunIndex = 0;
		unlockedGuns.Add(GunType.GlockPro);
	}
}
