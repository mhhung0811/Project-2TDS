using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    public CircleCollider2D interactCollider;
    private List<IInteractable> interactablesInZone = new List<IInteractable>();
    
    [Header("Gizmos")]
    [SerializeField] private bool showInteractCollider = true;
    [SerializeField] private Color interactionColor = Color.green;

    private void Start()
    {
        interactCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null && !interactablesInZone.Contains(interactable))
        {
            interactable.isInteractable = true; // Mark as interactable
            interactablesInZone.Add(interactable);
            // Debug.Log($"{other.gameObject.name} entered the interaction zone and is now interactable.");
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null && interactablesInZone.Contains(interactable))
        {
            interactable.isInteractable = false; // Mark as non-interactable
            interactablesInZone.Remove(interactable);
            // Debug.Log($"{other.gameObject.name} exited the interaction zone and is no longer interactable.");
        }
    }
    
    public void Interact(GameObject go)
    {
        foreach (var interactable in interactablesInZone)
        {
            if (interactable.isInteractable)
            {
                interactable.Interact(go); // Interact with the object
                return; // Stop after interacting with the first valid object
            }
        }

        Debug.Log("No interactable objects in the zone.");
    }

    private void OnDrawGizmos()
    {
        if (interactCollider != null && showInteractCollider)
        {
            Gizmos.color = interactionColor;
            Gizmos.DrawWireSphere(interactCollider.transform.position, interactCollider.radius);
        }
    }
}