using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InUseGun", menuName = "Data Object/InUseGun")]
public class InUseGun : ScriptableObject
{
    [Serializable]
    public class GunEntry 
    {
        public int gunId;
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
            gunEntry.gunPrefab.GetComponent<GunBase>().SetGunId(gunEntry.gunId);
            gunEntry.gunPref.GetComponent<GunPref>().SetGunId(gunEntry.gunId);
            gunEntry.gunUI.GetComponent<GunUI>().SetGunId(gunEntry.gunId);
            gunEntry.gunBulletShellUI.GetComponent<BulletShellUI>().SetGunId(gunEntry.gunId);
        }
    }

    // Retrieve Gun by ID
    public GameObject GetGunById(int id)
    {
        foreach (var gunEntry in guns)
        {
            if (gunEntry.gunId == id)
                return gunEntry.gunPrefab;
        }

        Debug.LogError($"Gun with ID {id} not found!");
        return null;
    }
    
    // Retrieve GunPref by ID
    public GameObject GetGunPrefById(int id)
    {
        foreach (var gunEntry in guns)
        {
            if (gunEntry.gunId == id)
                return gunEntry.gunPref;
        }

        Debug.LogError($"Gun pref with ID {id} not found!");
        return null;
    }
    
    // Retrieve GunUI by ID
    public GameObject GetGunUIById(int id)
    {
        foreach (var gunEntry in guns)
        {
            if (gunEntry.gunId == id)
                return gunEntry.gunUI;
        }

        Debug.LogError($"Gun UI with ID {id} not found!");
        return null;
    }
    
    // Retrieve BulletShellUI by ID
    public GameObject GetBulletShellUIById(int id)
    {
        foreach (var gunEntry in guns)
        {
            if (gunEntry.gunId == id)
                return gunEntry.gunBulletShellUI;
        }

        Debug.LogError($"Bullet shell UI with ID {id} not found!");
        return null;
    }
}