using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveSlot
{
    public string slotName;
    public string lastGame;

    public SaveSlot(string slotName)
	{
		this.slotName = slotName;
		lastGame = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
	}
}
