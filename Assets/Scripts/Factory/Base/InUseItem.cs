using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InUseItem", menuName = "Data Object/InUseItem")]
public class InUseItem : ScriptableObject
{
    [Serializable]
    public class ItemEntry
    {
        public ItemType itemType;
        public GameObject itemPrefab;
        public GameObject itemUI;
    }
    
    public List<ItemEntry> items = new();

    public GameObject GetItemByType(ItemType type)
    {
        foreach (var itemEntry in items)
        {
            if (itemEntry.itemType == type)
                return itemEntry.itemPrefab;
        }
        
        Debug.LogError($"Item with type {type} not found!");
        return null;
    }
    
    public GameObject GetItemUIByType(ItemType type)
    {
        foreach (var itemEntry in items)
        {
            if (itemEntry.itemType == type)
                return itemEntry.itemUI;
        }
        
        Debug.LogError($"Item UI with type {type} not found!");
        return null;
    }
}