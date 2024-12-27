using UnityEngine;

public interface IInteractable
{
    public bool isInteractable { get; set; }
    void Interact(GameObject go);
}