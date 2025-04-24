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

	void Start()
    {
        
    }

    void Update()
    {
        
    }

	private void OnEnable()
	{
		GameManager.Instance.isOpenUI = true;
	}

	private void OnDisable()
	{
		GameManager.Instance.isOpenUI = false;
	}	
	public void OnClickBtnRest()
    {
		Debug.Log("Resting...");
		playerHp.CurrentValue = playerMaxHp.CurrentValue;
		playerMana.CurrentValue = playerMaxMana.CurrentValue;
		gameObject.SetActive(false);
	}

    public void OnClickBtnInventory()
    {
        gameObject.SetActive(false);
	}

    public void OnClickBtnSaveGame()
    {
		Debug.Log("Saving game...");
		SaveGameManager.Instance.gameData.health = playerHp.CurrentValue;
        SaveGameManager.Instance.gameData.maxHealth = playerMaxHp.CurrentValue;
		SaveGameManager.Instance.gameData.mana = playerMana.CurrentValue;
		SaveGameManager.Instance.gameData.maxMana = playerMaxMana.CurrentValue;
		SaveGameManager.Instance.gameData.LastSpawn = playerPos.CurrentValue;
		SaveGameManager.Instance.SaveGame(SaveGameManager.Instance.gameData);
		gameObject.SetActive(false);
	}
}
