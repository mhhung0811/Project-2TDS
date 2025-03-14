using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSaveSlotUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public int slotWith = 500;
	public int slotHeight = 800;
	void Start()
    {
        List<SaveSlot> slotsList = SaveGameManager.Instance.GetSaveSlots();
		CreateSaveSlot(slotsList);
	}

    public void CreateSaveSlot(List<SaveSlot> slotsList) { 
        int count = slotsList.Count;
        // set size panel
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(slotWith * count, slotHeight);
        this.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2((slotWith*count)/2, 0);

		float posXStart = slotWith / 2;
        // Instance slot
        for (int i = 0; i < count; i++)
        {
            float posX = posXStart + (slotWith * i);
			GameObject slot = Instantiate(slotPrefab, this.transform);

			RectTransform slotRect = slot.GetComponent<RectTransform>();
			slotRect.anchoredPosition = new Vector2(posX, 0);
			SaveSlotUI slotUI = slot.GetComponent<SaveSlotUI>();
            slotUI.saveSlot = slotsList[i];
		}
	}
}
