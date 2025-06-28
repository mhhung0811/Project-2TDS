using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class InventoryPanelUI : MonoBehaviour
    {
        [SerializeField] private InUseGun inUseGun;
        [SerializeField] private GunTypeIntEvent gunArsenalChange;
        [SerializeField] private IntEvent gunArsenalRemove;

        [Header("Gun")] 
        [SerializeField] private RectTransform gun2Slot;
        [SerializeField] private RectTransform gun3Slot;
        [SerializeField] private RectTransform gunPanel;
        [SerializeField] private RectTransform gunHolderPrefab;
        [SerializeField] private float gunUIScale = 2f;
        
        private List<RectTransform> gunEquipUIList = new();

        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitGunEquip();
            InitGunInventory();
        }

        private void InitGunInventory()
        {
            // Clear existing gun holders
            foreach (Transform child in gunPanel.transform)
            {
                Destroy(child.gameObject);
            }

            // Create new gun holders based on the current guns
            for (int i = 0; i < SaveGameManager.Instance.gameData.unlockedGuns.Count; i++)
            {
                var type = SaveGameManager.Instance.gameData.unlockedGuns[i];
                
                var gunHolder = Instantiate(gunHolderPrefab, gunPanel.transform);
                gunHolder.GetComponent<GunInventoryHolderUI>().Initialize(this, i);
                var gunUI = Instantiate(inUseGun.GetGunUIByType(type), gunHolder.transform);
                var rect = gunUI.GetComponent<RectTransform>();
                rect.localPosition *= gunUIScale;
                rect.sizeDelta *= gunUIScale;
            }
        }
        private void InitGunEquip()
        {
            gun2Slot.gameObject.SetActive(false);
            gun3Slot.gameObject.SetActive(false);
            var index = 0;
            switch (SaveGameManager.Instance.gameData.gunSlots)
            {
                case 2:
                    gunEquipUIList.Clear();
                    // Clear existing gun holders
                    foreach (Transform child in gun2Slot.transform)
                    {
                        var rect = child.GetComponent<RectTransform>();
                        Debug.Log(this.name + " - Initializing gun holder at index: " + index);
                        rect.GetComponent<GunEquipHolderUI>().Initialize(this, index);
                        foreach (Transform r in rect)
                        {
                            Destroy(r.gameObject);
                        }
                        gunEquipUIList.Add(child.GetComponent<RectTransform>());
                        index++;
                    }
                    // Create new gun holders based on the current guns
                    for (int i = 0; i < SaveGameManager.Instance.gameData.currentGuns.Count; i++)
                    {
                        if (i >= SaveGameManager.Instance.gameData.gunSlots) break;
                        var type = SaveGameManager.Instance.gameData.currentGuns[i];
                        if (inUseGun.GetGunUIByType(type) == null)
                        {
                            Debug.LogError($"Gun UI of type {type} not found!");
                            continue;
                        }
                        
                        var gunUI = Instantiate(inUseGun.GetGunUIByType(type), gunEquipUIList[i].transform);
                        var rect = gunUI.GetComponent<RectTransform>();
                        
                        rect.localPosition *= gunUIScale;
                        rect.sizeDelta *= gunUIScale;
                    }
                    
                    gun2Slot.gameObject.SetActive(true);
                    
                    break;
                case 3:
                    gunEquipUIList.Clear();
                    // Clear existing gun holders
                    foreach (Transform child in gun3Slot.transform)
                    {
                        var rect = child.GetComponent<RectTransform>();
                        rect.GetComponent<GunEquipHolderUI>().Initialize(this, index);
                        foreach (Transform r in rect)
                        {
                            Destroy(r.gameObject);
                        }
                        gunEquipUIList.Add(child.GetComponent<RectTransform>());
                        index++;
                    }
                    // Create new gun holders based on the current guns
                    for (int i = 0; i < SaveGameManager.Instance.gameData.currentGuns.Count; i++)
                    {
                        if (i >= SaveGameManager.Instance.gameData.gunSlots) break;
                        var type = SaveGameManager.Instance.gameData.currentGuns[i];
                        if (inUseGun.GetGunUIByType(type) == null)
                        {
                            Debug.LogError($"Gun UI of type {type} not found!");
                            continue;
                        }
                        
                        var gunUI = Instantiate(inUseGun.GetGunUIByType(type), gunEquipUIList[i].transform);
                        var rect = gunUI.GetComponent<RectTransform>();
                        
                        rect.localPosition *= gunUIScale;
                        rect.sizeDelta *= gunUIScale;
                    }
                    
                    gun3Slot.gameObject.SetActive(true);
                    break;
            }
        }

        public void EquipGun(int index)
        {
            var gunToEquip = SaveGameManager.Instance.gameData.unlockedGuns[index];

            // Check for duplicate in the UI layer
            foreach (var slot in gunEquipUIList)
            {
                if (slot.childCount > 0)
                {
                    var existingGun = slot.GetChild(0).GetComponent<GunUI>();
                    if (existingGun != null && existingGun.type.Equals(gunToEquip))
                    {
                        Debug.LogWarning($"Gun of type {gunToEquip} is already shown in UI.");
                        return;
                    }
                }
            }

            // Find the first empty slot in UI
            for (int i = 0; i < gunEquipUIList.Count; i++)
            {
                var slot = gunEquipUIList[i];
                if (slot.childCount == 0)
                {
                    var prefab = inUseGun.GetGunUIByType(gunToEquip);
                    if (prefab == null)
                    {
                        Debug.LogError($"Gun UI of type {gunToEquip} not found!");
                        return;
                    }

                    var gunUI = Instantiate(prefab, slot);
                    var rect = gunUI.GetComponent<RectTransform>();
                    rect.localPosition *= gunUIScale;
                    rect.sizeDelta *= gunUIScale;
                    return;
                }
            }

            Debug.LogWarning("No empty UI slot available.");
        }
        
        public void UnEquipGun(int index)
        {
            if (index < 0 || index >= gunEquipUIList.Count)
            {
                Debug.LogWarning("Invalid index to unequip.");
                return;
            }

            var slot = gunEquipUIList[index];
            if (slot.childCount > 0)
            {
                Destroy(slot.GetChild(0).gameObject);
            }

            // Shift UI left to fill gap
            for (int i = index + 1; i < gunEquipUIList.Count; i++)
            {
                var src = gunEquipUIList[i];
                var dst = gunEquipUIList[i - 1];

                if (src.childCount > 0)
                {
                    var moved = src.GetChild(0);
                    var rect = moved.GetComponent<RectTransform>();
                    var pos = rect.localPosition;

                    moved.SetParent(dst);
                    rect.localPosition = pos;
                    rect.localScale = Vector3.one;
                }
                else
                {
                    break;
                }
            }

            // Clear the last UI slot
            var lastSlot = gunEquipUIList[gunEquipUIList.Count - 1];
            if (lastSlot.childCount > 0)
            {
                Destroy(lastSlot.GetChild(0).gameObject);
            }
        }

        public void OnClickButtonClose()
        {
            var gameData = SaveGameManager.Instance.gameData;
            gameData.currentGuns.Clear(); // ← rebuild fresh

            int nonEmptyCount = 0;

            for (int i = 0; i < gunEquipUIList.Count; i++)
            {
                var slot = gunEquipUIList[i];

                if (slot.childCount > 0)
                {
                    nonEmptyCount++;

                    var gunUI = slot.GetChild(0).GetComponent<GunUI>();
                    if (gunUI == null)
                    {
                        Debug.LogWarning($"GunUI not found in equip slot {i}.");
                        // gameData.currentGuns.Add(null);
                        continue;
                    }

                    gameData.currentGuns.Add(gunUI.type);
                    gunArsenalChange?.Raise((gunUI.type, i));
                }
                else
                {
                    // gameData.currentGuns.Add(null);
                    gunArsenalRemove?.Raise(i);
                }
            }

            // If all are empty, fallback to first unlocked gun
            if (nonEmptyCount == 0 && gameData.unlockedGuns.Count > 0)
            {
                var fallback = gameData.unlockedGuns[0];
                gameData.currentGuns.Clear();
                gameData.currentGuns.Add(fallback);
                gunArsenalChange?.Raise((fallback, 0));
            }

            SaveGameManager.Instance.SaveGame(gameData);
            gameObject.SetActive(false);
        }
    }
}