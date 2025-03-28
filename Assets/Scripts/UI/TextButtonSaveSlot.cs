using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextButtonSaveSlot : MonoBehaviour
{
    public TextMeshProUGUI text;
	private void Awake()
	{
		text = this.GetComponent<TextMeshProUGUI>();
	}

	void Start()
    {
		text = this.GetComponent<TextMeshProUGUI>();
		text.text = "Save: " + SaveGameManager.Instance.currentSaveSlot.slotName.Replace("slot_", "Slot ");
	}

    public void UpdateTextWhenClickSaveSlot()
	{
		text.text = "Save: " + SaveGameManager.Instance.currentSaveSlot.slotName.Replace("slot_", "Slot ");
	}
}
