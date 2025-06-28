using System.Collections.Generic;
using UnityEngine;

public class PlayerArsenal : MonoBehaviour
{
    private int _gunSlotSize;
    private List<GunBase> gunCollection = new();

    private int _currentGunIndex;
    
    public Transform gunHolder;
    
    public GunTypeIntEvent onGunChange;
    
    [Header("Variables")]
    public FloatVariable playerMaxReloadTime;
    public FloatVariable playerReloadTime;
    public BoolVariable playerIsReloading;
    public IntVariable playerAmmo;
    public IntVariable playerTotalAmmo;
    public Vector2Variable holdGunPos;

    private void Awake()
    {
        // Retrieve data
        _currentGunIndex = -1;
        _gunSlotSize = 3;
    }

    // Event listener
    public void ChangeGunInArsenal((GunBase gun, int index) gunInfo)
    {
        if (gunInfo.index >= _gunSlotSize || gunInfo.index < 0)
        {
            Debug.LogError($"Invalid index {gunInfo.index}. Cannot change gun in arsenal.");
            return;
        }
        
        // Move from pool to player
        gunInfo.gun.transform.SetParent(gunHolder);
        gunInfo.gun.transform.localPosition = new Vector3(0.3f, 0, 0);
        gunInfo.gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (gunCollection.Count > gunInfo.index)
        {
            gunCollection[gunInfo.index] = gunInfo.gun;
        }
        else
        {
            gunCollection.Add(gunInfo.gun);
        }
        
        // Auto switch to the first gun
        SwitchGun(0);
    }
    
    // Event listener
    /// <summary>
    /// Removes a gun from the arsenal by index.
    /// </summary>
    public void RemoveGunInArsenal(int index)
    {
        if (index < 0 || index >= gunCollection.Count)
        {
            Debug.LogWarning($"Invalid gun index: {index}. Cannot remove.");
            return;
        }

        var gunToRemove = gunCollection[index];

        if (gunToRemove != null)
        {
            Debug.Log("Still work");
            // gunToRemove.ResetReloadTimeVariables();
            // gunToRemove.ResetAmmoVariables();
            // gunToRemove.ResetTotalAmmoVariable();
            gunToRemove.gameObject.SetActive(false);
            gunToRemove.transform.SetParent(null); // optionally detach it from gunHolder
        }

        // Remove the gun and shift the rest
        gunCollection.RemoveAt(index);

        // Adjust currentGunIndex
        if (_currentGunIndex == index)
        {
            _currentGunIndex = -1;
            AutoSwitchNextValidGun();
        }
        else if (_currentGunIndex > index)
        {
            _currentGunIndex--; // account for shifted elements
        }
    }
    
    private void AutoSwitchNextValidGun()
    {
        for (int i = 0; i < gunCollection.Count; i++)
        {
            if (gunCollection[i] != null)
            {
                SwitchGun(i);
                return;
            }
        }

        Debug.Log("No guns available to switch to.");
    }

    /// <summary>
    /// Switches to a gun based on its index in the collection.
    /// </summary>
    public void SwitchGun(int gunIndex)
    {
        if (gunIndex < 0 || gunIndex >= gunCollection.Count || gunCollection[gunIndex] == null)
        {
            Debug.LogWarning($"Invalid gun index: {gunIndex}. Cannot switch to that gun.");
            return;
        }

        EquipGun(gunIndex);
    }

    /// <summary>
    /// Equips a gun from the inventory as the holding gun.
    /// </summary>
    private void EquipGun(int gunIndex)
    {
        var holdingGun = GetHoldingGun();
        
        // Check if the gun index is valid
        if (gunCollection[gunIndex] == null)
        {
            Debug.LogWarning($"Gun at index {gunIndex} is null. Cannot equip.");
            return;
        }
        var gun = gunCollection[gunIndex];
        
        // Check if the gun is already equipped
        if (gunIndex == _currentGunIndex)
        {
            holdingGun.gameObject.SetActive(true);
            
            Debug.LogWarning($"Already equipped {holdingGun.name}.");
            return;
        }
        
        // Notify listeners that the gun has changed
        onGunChange?.Raise((gun.gunType, gun.maxAmmoPerMag));
        
        // Deactivate the currently held gun
        if (holdingGun != null)
        {
            holdingGun.ResetReloadTimeVariables();
            holdingGun.ResetAmmoVariables();
            holdingGun.ResetTotalAmmoVariable();
            holdingGun.gameObject.SetActive(false);
        }
        
        // Equip the new gun
        _currentGunIndex = gunIndex;
        gun.SetUpReloadTimeVariables(playerMaxReloadTime, playerReloadTime, playerIsReloading);
        gun.SetUpAmmoVariables(playerAmmo);
        gun.SetUpTotalAmmoVariable(playerTotalAmmo);
        gun.transform.localPosition = gun.posGun;
        holdGunPos.CurrentValue = gun.posHoldGun;
        gun.gameObject.SetActive(true);

        //Set Scale x >0, y > 0
        gun.transform.localScale = new Vector3(1, 1, 1);        
        
        // Debug.Log($"Equipped {holdingGun.name}.");
    }

    /// <summary>
    /// Gets the currently held gun.
    /// </summary>
    public GunBase GetHoldingGun()
    {
        if (_currentGunIndex == -1) return null;
        
        if (gunCollection[_currentGunIndex] == null)
        {
            Debug.Log("No gun is currently equipped.");
        }
        
        return gunCollection[_currentGunIndex];
    }
}