using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InUseGun", menuName = "Data Object/InUseGun")]
public class InUseGun : ScriptableObject
{
    [Serializable]
    public class GunEntry 
    {
        public GunType gunType;
        public GameObject gunPrefab;
        public GameObject gunPref;
        public GameObject gunUI;
        public GameObject gunBulletShellUI;
    }
    
    public List<GunEntry> guns = new List<GunEntry>();

    private void Awake()
    {
        foreach (var gunEntry in guns)
        {
            // gunEntry.gunPrefab.GetComponent<GunBase>().SetGunId(gunEntry.gunId);
            // gunEntry.gunPref.GetComponent<GunPref>().SetGunId(gunEntry.gunId);
            // gunEntry.gunUI.GetComponent<GunUI>().SetGunId(gunEntry.gunId);
            // gunEntry.gunBulletShellUI.GetComponent<BulletShellUI>().SetGunId(gunEntry.gunId);
        }
    }
    
    public GameObject GetGunByType(GunType type)
    {
        foreach (var gunEntry in guns)
        {
            if (gunEntry.gunType == type)
                return gunEntry.gunPrefab;
        }

        Debug.LogError($"Gun with type {type} not found!");
        return null;
    }
    
    public GameObject GetGunPrefByType(GunType type)
    {
        foreach (var gunEntry in guns)
        {
            if (gunEntry.gunType == type)
                return gunEntry.gunPref;
        }

        Debug.LogError($"Gun pref with type {type} not found!");
        return null;
    }
    
    public GameObject GetGunUIByType(GunType type)
    {
        foreach (var gunEntry in guns)
        {
            if (gunEntry.gunType == type)
                return gunEntry.gunUI;
        }

        Debug.LogError($"Gun UI with type {type} not found!");
        return null;
    }
    
    public GameObject GetBulletShellUIByType(GunType type)
    {
        foreach (var gunEntry in guns)
        {
            if (gunEntry.gunType == type)
                return gunEntry.gunBulletShellUI;
        }

        Debug.LogError($"Bullet shell UI with type {type} not found!");
        return null;
    }
}