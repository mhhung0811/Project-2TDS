using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameManager : Singleton<SaveGameManager>
{
	private string _saveDirection;
	private string _saveListPath;
	private string _preName;

	[NonSerialized]
	public bool isGameLoaded;
	[NonSerialized]
	public GameData gameData;
	[NonSerialized]
	public SaveSlot currentSaveSlot;

	public override void Awake()
	{
		base.Awake();

		_saveDirection = Application.persistentDataPath + "/saves/";
		_saveListPath = _saveDirection + "save.json";
		_preName = "slot_";

		if (!Directory.Exists(_saveDirection))
		{
			Directory.CreateDirectory(_saveDirection);
			Debug.Log("SaveGameManager: " + _saveDirection);
			if (!File.Exists(_saveListPath))
			{
				int count = 7;
				List<SaveSlot> saveSlots = new List<SaveSlot>();
				for (int i = 1; i <= count; i++)
				{
					saveSlots.Add(new SaveSlot(_preName + i));
				}
				SaveSlotList saveSlotList = new SaveSlotList { saveSlots = saveSlots };
				File.WriteAllText(_saveListPath, JsonUtility.ToJson(saveSlotList, true));
				Debug.Log("SaveGameManager: CreateSaveSlots: Save slots created");
			}
		}

		currentSaveSlot = GetSaveSlots()[0];
		Debug.Log(_saveDirection);
		Debug.Log("SaveGameManager: Start: Current save slot: " + currentSaveSlot.slotName);

		LoadGame();
	}

	// Get all save slots
	public List<SaveSlot> GetSaveSlots()
	{
		if(File.Exists(_saveListPath))
		{
			string json = File.ReadAllText(_saveListPath);
			return JsonUtility.FromJson<SaveSlotList>(json).saveSlots;
		}
		return new List<SaveSlot>();
	}

	// Create a new save slot
	public SaveSlot CreateNewSlot()
	{
		List<SaveSlot> saveSlots = GetSaveSlots();
		int maxSlotId = 0;
		foreach (SaveSlot saveSlot in saveSlots)
		{
			string slotId = saveSlot.slotName.Replace(_preName, "");
			int id = int.Parse(slotId);
			if (id > maxSlotId)
			{
				maxSlotId = id;
			}
		}
		int newSlotId = maxSlotId + 1;
		string slotName = _preName + newSlotId;

		saveSlots.Add(new SaveSlot(slotName));
		SaveSlotList saveSlotList = new SaveSlotList { saveSlots = saveSlots};
		File.WriteAllText(_saveListPath, JsonUtility.ToJson(saveSlotList,true));

		return saveSlots[saveSlots.Count - 1];
	}

	// save game data to save slot
	public void SaveGame(GameData gameData)
	{
		string slotName = currentSaveSlot.slotName;
		string path = _saveDirection + slotName + ".json";
		File.WriteAllText(path, JsonUtility.ToJson(gameData, true));
		Debug.Log("SaveGameManager: SaveGame: Game saved to " + path);
	}

	// load game data from save slot
	public void LoadGame()
	{
		string slotName = currentSaveSlot.slotName;
		string path = _saveDirection + slotName + ".json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			this.gameData = JsonUtility.FromJson<GameData>(json);
			Debug.Log("SaveGameManager: LoadGame: Game loaded from " + path);
			isGameLoaded = true;
		}
	}

	// delete all files in the save directory
	public void DeleteAllSaves()
	{
		DirectoryInfo di = new DirectoryInfo(_saveDirection);
		foreach (FileInfo file in di.GetFiles())
		{
			file.Delete();
		}
		Debug.Log("SaveGameManager: DeleteAllSaves: All files deleted");
	}

	// Check file exists and not empty
	public bool CheckFile(string slotName)
	{
		string path = _saveDirection + slotName + ".json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			if (json != "")
			{
				return true;
			}
		}
		return false;
	}

	// Check the current save slot file
	public bool CheckCurrentSaveSlot()
	{
		if (currentSaveSlot != null)
		{
			string path = _saveDirection + currentSaveSlot.slotName + ".json";
			if (File.Exists(path))
			{
				string json = File.ReadAllText(path);
				if (json != "")
				{
					return true;
				}
			}
		}
		return false;
	}
}

[System.Serializable]
public class SaveSlotList
{
	public List<SaveSlot> saveSlots = new();
}
