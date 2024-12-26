using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<GunBase> gunCollection = new List<GunBase>();
    
    private GunBase holdingGun;
    
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

        gunCollection.Add(newGun);
        Debug.Log($"Added {newGun.name} to inventory.");
    }
    
    /// <summary>
    /// Removes a gun from the player's inventory.
    /// </summary>
    public void RemoveGun(GunBase gun)
    {
        if (gunCollection.Remove(gun))
        {
            Debug.Log($"Removed {gun.name} from inventory.");
        }
        else
        {
            Debug.LogError($"Failed to remove {gun.name}: Gun not found in inventory.");
        }
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

        holdingGun = gun;
        Debug.Log($"Equipped {holdingGun.name}.");
    }
    
    /// <summary>
    /// Unequips the currently held gun.
    /// </summary>
    public void UnequipGun()
    {
        if (holdingGun != null)
        {
            Debug.Log($"Unequipped {holdingGun.name}.");
            holdingGun = null;
        }
        else
        {
            Debug.LogError("No gun is currently equipped.");
        }
    }
    
    /// <summary>
    /// Gets the currently held gun.
    /// </summary>
    public GunBase GetHoldingGun()
    {
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