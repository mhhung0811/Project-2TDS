using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameManager : Singleton<SaveGameManager>
{
	private string saveDirection;
	private string saveListPath;
	private string preName;

	public GameData gameData;
	public SaveSlot currentSaveSlot;

	public override void Awake()
	{
		base.Awake();

		saveDirection = Application.persistentDataPath + "/saves/";
		saveListPath = saveDirection + "save.json";
		preName = "slot_";

		if (!Directory.Exists(saveDirection))
		{
			Directory.CreateDirectory(saveDirection);
			Debug.Log("SaveGameManager: " + saveDirection);
			if (!File.Exists(saveListPath))
			{
				int count = 7;
				List<SaveSlot> saveSlots = new List<SaveSlot>();
				for (int i = 1; i <= count; i++)
				{
					saveSlots.Add(new SaveSlot(preName + i));
				}
				SaveSlotList saveSlotList = new SaveSlotList { saveSlots = saveSlots };
				File.WriteAllText(saveListPath, JsonUtility.ToJson(saveSlotList, true));
				Debug.Log("SaveGameManager: CreateSaveSlots: Save slots created");
			}
		}
	}

	// get all save slots
	public List<SaveSlot> GetSaveSlots()
	{
		if(File.Exists(saveListPath))
		{
			string json = File.ReadAllText(saveListPath);
			return JsonUtility.FromJson<SaveSlotList>(json).saveSlots;
		}
		return new List<SaveSlot>();
	}

	// create new save slot
	public SaveSlot CreateNewSlot()
	{
		List<SaveSlot> saveSlots = GetSaveSlots();
		int maxSlotId = 0;
		foreach (SaveSlot saveSlot in saveSlots)
		{
			string slotId = saveSlot.slotName.Replace(preName, "");
			int id = int.Parse(slotId);
			if (id > maxSlotId)
			{
				maxSlotId = id;
			}
		}
		int newSlotId = maxSlotId + 1;
		string slotName = preName + newSlotId;

		saveSlots.Add(new SaveSlot(slotName));
		SaveSlotList saveSlotList = new SaveSlotList { saveSlots = saveSlots};
		File.WriteAllText(saveListPath, JsonUtility.ToJson(saveSlotList,true));

		return saveSlots[saveSlots.Count - 1];
	}

	// save game data to save slot
	public void SaveGame(string slotName, GameData gameData)
	{
		string path = saveDirection + slotName + ".json";
		File.WriteAllText(path, JsonUtility.ToJson(gameData, true));
		Debug.Log("SaveGameManager: SaveGame: Game saved to " + path);
	}

	// load game data from save slot
	public GameData LoadGame(string slotName)
	{
		string path = saveDirection + slotName + ".json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			this.gameData = JsonUtility.FromJson<GameData>(json);
			return this.gameData;
		}
		Debug.LogWarning("SaveGameManager: LoadGame: File not found");
		return null;
	}

	// delete save slot
	public void DeleteSaveSlot(string slotName)
	{
		string path = saveDirection + slotName + ".json";
		if(File.Exists(path))
		{
			File.Delete(path);
			Debug.Log("SaveGameManager: DeleteSave: Save deleted");
			//if (File.Exists(saveListPath))
			//{
			//	List<SaveSlot> saveSlots = GetSaveSlots();
			//	saveSlots.RemoveAll(x => x.slotName == slotName);
			//	SaveSlotList saveSlotList = new SaveSlotList { saveSlots = saveSlots };
			//	File.WriteAllText(saveListPath, JsonUtility.ToJson(saveSlotList, true));
			//}
		}
		else
		{
			Debug.LogWarning("SaveGameManager: DeleteSave: File not found");
		}
	}

	// delete all files in save directory
	public void DeleteAllSaves()
	{
		DirectoryInfo di = new DirectoryInfo(saveDirection);
		foreach (FileInfo file in di.GetFiles())
		{
			file.Delete();
		}
		Debug.Log("SaveGameManager: DeleteAllSaves: All files deleted");
	}

	// Check file exists and not empty
	public bool CheckFile(string slotName)
	{
		string path = saveDirection + slotName + ".json";
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
}

[System.Serializable]
public class SaveSlotList
{
	public List<SaveSlot> saveSlots = new List<SaveSlot>();
}
