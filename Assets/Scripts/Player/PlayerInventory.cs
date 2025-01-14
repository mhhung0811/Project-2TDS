using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<GunBase> gunCollection = new List<GunBase>();
    
    private GunBase holdingGun;
 
    public int gunInventorySize = 3;
    
    public Transform gunHolder;
    
    public GameObjectIntFuncProvider getGunFunc;
    public VoidGameObjectFuncProvider returnGunFunc;
    public VoidIntVector2FloatFuncProvider getGunPrefFunc;
    public IntEvent onGunChange;
    public FloatVariable playerMaxReloadTime;
    public FloatVariable playerReloadTime;
    public BoolVariable isReloading;

    private void Start()
    {
        // Get gun id 1
        var gun = getGunFunc.GetFunction()?.Invoke((2));
        if (gun != null && gun.GetComponent<GunBase>() != null)
        {
            // Debug.Log($"Add {gun.GetComponent<GunBase>().gunName}.");
            // Debug.Log($"Add {gun.GetComponent<GunBase>().gunId}.");
            AddGun(gun.GetComponent<GunBase>());
        }
        else
        {
            Debug.LogError("Failed to get gun from gun pref.");
        }
    }

    /// <summary>
    /// Adds a gun to the player's inventory.
    /// </summary>
    public void AddGun(GunBase newGun)
    {
        if (newGun == null)
        {
            Debug.LogError("Cannot add a null gun to the inventory!");
            return;
        }

        // Check if the inventory is full
        if (gunCollection.Count >= gunInventorySize)
        {
            Debug.LogWarning($"Inventory full! Dropping the currently held gun to add {newGun.name}.");

            // Drop the currently held gun to make room for the new gun
            DropGun();
        }
        
        // Move from pool to player
        newGun.transform.SetParent(gunHolder);
        newGun.transform.localPosition = new Vector3(0.3f, 0, 0);
        newGun.transform.localRotation = Quaternion.Euler(0, 0, 0);
        
        gunCollection.Add(newGun);
        EquipGun(newGun);
        // Debug.Log($"Added {newGun.name} to inventory.");
        // Debug.Log($"Added {newGun.gunId} to inventory.");
    }
    
    /// <summary>
    /// Drop holding gun from the player's inventory.
    /// </summary>
    public void DropGun()
    {
        if (holdingGun != null)
        {
            // Prevent dropping the last gun
            if (gunCollection.Count == 1)
            {
                Debug.LogWarning("Cannot drop the last gun in the inventory!");
                return;
            }
            
            if (gunCollection.Remove(holdingGun))
            {
                Debug.Log($"Drop {holdingGun.name}.");
                Debug.Log($"Drop {holdingGun.gunId}.");
                
                // Return gun and generate gun pref
                getGunPrefFunc.GetFunction()?.Invoke((holdingGun.gunId, transform.position, 0));
                returnGunFunc.GetFunction()?.Invoke(holdingGun.gameObject);
                
                holdingGun = null;
                
                // Equip the next available gun in the inventory, if any
                if (gunCollection.Count > 0)
                {
                    EquipGun(gunCollection[0]);
                }
                else
                {
                    Debug.Log("No guns left in inventory.");
                }
            }
            else
            {
                Debug.LogError($"Failed to remove {holdingGun.name}: Gun not found in inventory.");
            }
        }
        else
        {
            Debug.LogError("No gun is currently equipped.");
        }
    }
    
    /// <summary>
    /// Switches to a gun based on its index in the collection.
    /// </summary>
    public void SwitchGun(int gunIndex)
    {
        if (gunIndex < 0 || gunIndex >= gunCollection.Count)
        {
            Debug.LogWarning($"Invalid gun index: {gunIndex}. Cannot switch to that gun.");
            return;
        }

        EquipGun(gunCollection[gunIndex]);
    }
    
    /// <summary>
    /// Equips a gun from the inventory as the holding gun.
    /// </summary>
    public void EquipGun(GunBase gun)
    {
        if (!gunCollection.Contains(gun))
        {
            Debug.LogError($"Cannot equip {gun.name}: Gun is not in the inventory.");
            return;
        }

        // Deactivate the currently held gun
        if (holdingGun != null)
        {
            holdingGun.ResetReloadTimeVariables();
            holdingGun.gameObject.SetActive(false);
        }
        
        // Equip the new gun
        holdingGun = gun;
        holdingGun.SetUpReloadTimeVariables(playerMaxReloadTime, playerReloadTime, isReloading);
        holdingGun.gameObject.SetActive(true);

		//Set Scale x >0, y > 0
        holdingGun.transform.localScale = new Vector3(1, 1, 1);

		// Set position of gun
		holdingGun.transform.localPosition = new Vector3(0.5f, 0, 0);

        // Notify listeners that the gun has changed
        onGunChange?.Raise(holdingGun.gunId);
        
		// Debug.Log($"Equipped {holdingGun.name}.");
    }
    
    /// <summary>
    /// Gets the currently held gun.
    /// </summary>
    public GunBase GetHoldingGun()
    {
        if (holdingGun == null)
        {
            Debug.Log("No gun is currently equipped.");
        }
        return holdingGun;
    }

    /// <summary>
    /// Gets the entire gun collection.
    /// </summary>
    public List<GunBase> GetGunCollection()
    {
        return gunCollection;
    }
}