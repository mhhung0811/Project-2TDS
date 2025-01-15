using System;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private InUseGun inUseGun;
    [SerializeField] private IntVariable playerAmmo;
    [SerializeField] private GameObject shellHolder;
    [SerializeField] private GameObject topShell;
    [SerializeField] private GameObject bottomShell;
    
    private List<BulletShellUI> bulletShells = new List<BulletShellUI>();

    public void Awake()
    {
        playerAmmo.OnChanged += UpdateAmmoUI;
    }

    public void ChangeGun((int id, int maxAmmo) parameters)
    {
        // Debug.Log($"UI: {playerMaxAmmo.CurrentValue}");
        
        foreach (Transform child in shellHolder.transform)
        {
            bulletShells.Clear();
            Destroy(child.gameObject);
        }
        
        float cumulativePosY = 0f;
        
        for (int i = 0; i < parameters.maxAmmo; i++)
        {
            // Instantiate a new shell as a child of the shellHolder
            var shell = Instantiate(inUseGun.GetBulletShellUIById(parameters.id), shellHolder.transform);

            // Get the BulletShellUI component
            var shellUI = shell.GetComponent<BulletShellUI>();

            if (shellUI != null)
            {
                // Adjust the Y position of the shell based on shellDistant
                RectTransform shellRect = shell.GetComponent<RectTransform>();
                if (shellRect != null)
                {
                    cumulativePosY += shellUI.shellDistant; // Increment the cumulative Y position
                    Vector2 newPosition = shellRect.anchoredPosition;
                    newPosition.y = cumulativePosY; // Set the new Y position
                    shellRect.anchoredPosition = newPosition;
                }

                // Add the shellUI to the list for later use
                bulletShells.Add(shellUI);
            }
        }
        
        // Adjust the topShell position
        RectTransform topShellRect = topShell.GetComponent<RectTransform>();
        if (topShellRect != null)
        {
            Vector2 topShellPosition = topShellRect.anchoredPosition;
            topShellPosition.y = cumulativePosY + 18; // Position the topShell at the cumulative Y position
            topShellRect.anchoredPosition = topShellPosition;
        }
        
        // Debug.Log($"Total shells: {bulletShells.Count}");
        // Debug.Log($"Current ammo {playerAmmo.CurrentValue}");
    }
    
    public void UpdateAmmoUI(int value)
    {
        // Debug.Log($"UI: {playerAmmo.CurrentValue}");
        Debug.Log($"UI change: {value}");
        Debug.Log($"Shell count: {bulletShells.Count}");
        
        // Handle invalid input scenarios
        if (value < 0)
        {
            Debug.LogWarning("Ammo value cannot be negative. Setting to 0.");
            value = 0;
        }
        else if (value > bulletShells.Count)
        {
            Debug.LogWarning("Ammo value exceeds shell count. Clamping to maximum shell count.");
            value = bulletShells.Count;
        }
        
        for (int i = 0; i < bulletShells.Count; i++)
        {
            if (i < value)
            {
                bulletShells[i].IsFull = true; // Set shell as full
            }
            else
            {
                bulletShells[i].IsFull = false; // Set shell as empty
                // Debug.Log($"Shell {i} set to empty.");
            }
        }
    }
}