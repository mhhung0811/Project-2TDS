using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestArea : MonoBehaviour, IInteractable
{
	public bool isInteractable { get; set; }
    public VoidEvent openRestPanel;
	void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void Interact(GameObject go)
	{
		Debug.Log("Resting...");
		openRestPanel.Raise(new Void());
	}
}
