using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class GunInventoryHolderUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image gunImage;

        private int index;
        private InventoryPanelUI manager;

        public void Initialize(InventoryPanelUI manager, int index)
        {
            this.index = index;
            this.manager = manager;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            gunImage.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            gunImage.enabled = false;
        }

        public void OnClick()
        {
            manager.EquipGun(index);
        }
    }
}