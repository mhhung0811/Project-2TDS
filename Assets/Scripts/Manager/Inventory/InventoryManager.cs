using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // private int _gunSlotSize;
    private HashSet<GunType> _gunColection = new();
    private Dictionary<GunType, GunBase> _guns = new();
    
    public InUseGun inUseGun;
    
    public GunBaseIntEvent onGunArsenalChange;
    
    private void Awake()
    {
        // Retrieve data
        // _gunSlotSize = 3;
        // _gunColection.Add(GunType.GlockPro);
        // _gunColection.Add(GunType.AssaultRifle);
        // _gunColection.Add(GunType.ShotGun);
    }

    private void Start()
    {
        InitializedGun();
        
        ChangeGunInArsenal(_guns[GunType.GlockPro], 0);
        // ChangeGunInArsenal(_guns[GunType.AssaultRifle], 1);
		// ChangeGunInArsenal(_guns[GunType.ShotGun], 2);
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

    public void ChangeGunInArsenal(GunBase gun, int index)
    {
        onGunArsenalChange?.Raise((gun, index));
    }
}