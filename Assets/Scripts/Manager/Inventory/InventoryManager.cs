using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // private int _gunSlotSize;
    private HashSet<GunType> _gunColection = new();
    private Dictionary<GunType, GunBase> _guns = new();
    
    public InUseGun inUseGun;
    
    public GunBaseIntEvent onGunArsenalChange;
    public IntEvent onGunArsenalRemove;

    private void Start()
    {
        InitializedGun();
	}
    
    private void InitializedGun()
    {
        // Retrieve data
        foreach (var gunType in SaveGameManager.Instance.gameData.unlockedGuns)
        {
            _gunColection.Add(gunType);
        }
        
        foreach (var gunType in _gunColection)
        {
            var gun = Instantiate(inUseGun.GetGunByType(gunType));
            gun.SetActive(false);
            if (gun != null)
            {
                var gunBase = gun.GetComponent<GunBase>();
                if (gunBase != null)
                {
                    _guns.Add(gunType, gunBase);
                }
                else
                {
                    Debug.LogError($"GunBase component not found on {gun.name}");
                }
            }
            else
            {
                Debug.LogError($"Gun of type {gunType} not found!");
            }
        }

        for (int i = 0; i < SaveGameManager.Instance.gameData.currentGuns.Count; i++)
        {
            if (i >= SaveGameManager.Instance.gameData.gunSlots) break;
            var type = SaveGameManager.Instance.gameData.currentGuns[i];
            if (_guns.ContainsKey(type))
            {
                onGunArsenalChange?.Raise((_guns[type], i));
            }
            else
            {
                Debug.LogError($"Gun of type {type} not found in initialized guns!");
            }
        }
    }

    // Event listener
    public void UnlockGun(GunType gunType)
    {
        if (_gunColection.Contains(gunType))
        {
            Debug.Log($"Gun {gunType} already unlocked!");
            return;
        }
        
        var gun = inUseGun.GetGunByType(gunType);
        if (gun != null)
        {
            var gunBase = gun.GetComponent<GunBase>();
            if (gunBase != null)
            {
                _guns.Add(gunType, gunBase);
                Debug.Log($"Gun {gun.name} unlocked!");
            }
            else
            {
                Debug.LogError($"GunBase component not found on {gun.name}");
            }
        }
        else
        {
            Debug.LogError($"Gun of type {gunType} not found!");
        }
    }
    
    // Event listener
    public void ChangeGunInArsenal((GunType gunType, int index) param)
    {
        if (_guns.TryGetValue(param.gunType, out var gun))
        {
            onGunArsenalChange?.Raise((gun, param.index));
        }
        else
        {
            Debug.LogError($"Gun of type {param.gunType} not found in arsenal!");
        }
    }
    
    // Event listener
    public void RemoveGunInArsenal(int index)
    {
        onGunArsenalRemove?.Raise(index);
    }
}