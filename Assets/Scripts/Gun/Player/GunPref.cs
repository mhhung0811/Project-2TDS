﻿using UnityEngine;

public class GunPref : MonoBehaviour, IInteractable
{
    public int gunId { get; private set; }
    public bool isInteractable { get; set; }
    public VoidGameObjectFuncProvider returnGunPrefFunc;
    public GameObjectIntFuncProvider getGunFunc;
    
    public void SetGunId(int id)
    {
        gunId = id;
    }
    public void Interact(GameObject go)
    {
        // Get gun
        GameObject obj = getGunFunc.GetFunction()?.Invoke((gunId));
        if (obj != null && obj.GetComponent<GunBase>() != null)
        {
            go.GetComponent<PlayerInventory>().AddGun(obj.GetComponent<GunBase>());
        }
        else
        {
            Debug.LogError("Failed to get gun from gun pref.");
        }
        
        // Return gun pref
        returnGunPrefFunc.GetFunction()?.Invoke(gameObject);
    }
}