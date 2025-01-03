﻿using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InUseGun", menuName = "InUseGun")]
public class InUseGun : ScriptableObject
{
    [Serializable]
    public class GunEntry 
    {
        public int gunId;
        public GameObject gunPrefab;
        public GameObject gunPref;
    }
    
    public List<GunEntry> guns = new List<GunEntry>();

    private void Awake()
    {
        foreach (var gunEntry in guns)
        {
            gunEntry.gunPrefab.GetComponent<GunBase>().SetGunId(gunEntry.gunId);
            gunEntry.gunPref.GetComponent<GunPref>().SetGunId(gunEntry.gunId);
            
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
}