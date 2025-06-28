using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestArea : MonoBehaviour, IInteractable
{
	public Transform spawnPos;
	public bool isInteractable { get; set; }
    public VoidEvent openRestPanel;
    public Vector2Variable currentSpawnPos;

	public void Interact(GameObject go)
	{
		openRestPanel.Raise(new Void());
		currentSpawnPos.CurrentValue = spawnPos.position;
	}
}
