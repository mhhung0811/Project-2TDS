using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class GunEquipHolderUI : MonoBehaviour
    {
        private int index;
        private InventoryPanelUI manager;
        
        public void Initialize(InventoryPanelUI manager, int index)
        {
            this.manager = manager;
            this.index = index;
            
            GetComponent<Button>().onClick.AddListener(OnClick);
        }
        
        public void OnClick()
        {
            manager.UnEquipGun(index);
        }
    }
}