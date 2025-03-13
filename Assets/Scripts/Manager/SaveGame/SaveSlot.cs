using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveSlot
{
    public string slotName;
    public DateTime lastSave;

    public SaveSlot(string slotName)
	{
		this.slotName = slotName;
		lastSave = DateTime.Now;
	}
}
