using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPanel : MonoBehaviour
{
    public IntVariable playerHp;
	public IntVariable playerMaxHp;
    public IntVariable playerMana;
	public IntVariable playerMaxMana;
    public Vector2Variable playerPos;
    
	public IntVariable currentRoomIndex;
	
	[SerializeField] private VoidEvent onRest;

	public void OnClickBtnRest()
    {
		Debug.Log("Resting...");
		playerHp.CurrentValue = playerMaxHp.CurrentValue;
		playerMana.CurrentValue = playerMaxMana.CurrentValue;
		gameObject.SetActive(false);
		onRest?.Raise(new Void());
	}

    public void OnClickBtnInventory()
    {
        gameObject.SetActive(false);
	}

    public void OnClickBtnSaveGame()
    {
		Debug.Log("Saving game...");
        SaveGameManager.Instance.gameData.maxHealth = playerMaxHp.CurrentValue;
		SaveGameManager.Instance.gameData.maxMana = playerMaxMana.CurrentValue;
		SaveGameManager.Instance.gameData.lastSpawn = playerPos.CurrentValue;
		SaveGameManager.Instance.gameData.lastRoom = currentRoomIndex.CurrentValue;
		SaveGameManager.Instance.SaveGame(SaveGameManager.Instance.gameData);
		gameObject.SetActive(false);
	}
}
