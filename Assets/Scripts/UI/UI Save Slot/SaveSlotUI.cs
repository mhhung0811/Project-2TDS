using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveSlotUI : MonoBehaviour
{
    public SaveSlot saveSlot;
	public TextMeshProUGUI slotName;
    public TextMeshProUGUI lastGame;
    public GameObject frameActive;

    public GameObject panelData;
    public GameObject panelEmpty;
    public VoidEvent onClick;
	void Start()
    {
        Load();
    }

    public void Load()
    {
        if (saveSlot == null)
        {
            Debug.Log("SaveSlotUI: Load: Save slot is null");
            return;
        }

        slotName.text = saveSlot.slotName.Replace("slot_", "Slot ");
        frameActive.SetActive(false);
		if (SaveGameManager.Instance.currentSaveSlot != null && saveSlot.slotName == SaveGameManager.Instance.currentSaveSlot.slotName)
        {
            slotName.text = slotName.text + " (Active)";
            frameActive.SetActive(true);
        }

        if (SaveGameManager.Instance.CheckFile(saveSlot.slotName))
        {
			panelData.SetActive(true);
            lastGame.text = saveSlot.lastGame;
			panelEmpty.SetActive(false);
		}
        else
        {
			panelData.SetActive(false);
			panelEmpty.SetActive(true);
		}

		panelData.SetActive(true);
		lastGame.text = saveSlot.lastGame;
		panelEmpty.SetActive(false);
	}

	public void OnClick()
	{
        Debug.Log("SaveSlotUI: OnClick: " + saveSlot.slotName);
		SaveGameManager.Instance.currentSaveSlot = saveSlot;
        frameActive.SetActive(true);
        slotName.text = saveSlot.slotName.Replace("slot_", "Slot ") + " (Active)";

		Void @void = new Void();
		onClick.Raise(@void);
	}

    public void UnActive()
    {
        if(SaveGameManager.Instance.currentSaveSlot != null && saveSlot.slotName == SaveGameManager.Instance.currentSaveSlot.slotName)
		{
            return;
		}
		frameActive.SetActive(false);
		slotName.text = saveSlot.slotName.Replace("slot_", "Slot ");
	}
}
