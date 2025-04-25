using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestArea : MonoBehaviour, IInteractable
{
	public bool isInteractable { get; set; }
    public VoidEvent openRestPanel;

	public void Interact(GameObject go)
	{
		openRestPanel.Raise(new Void());
	}
}
