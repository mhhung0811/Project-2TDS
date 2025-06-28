using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class ArsenalSelectPanelUI : MonoBehaviour
    {
        [SerializeField] private GameObject itemPanel;
        [SerializeField] private GameObject weaponPanel;
        [SerializeField] private GameObject backgroundPanel;

        private bool isItemPanelOn = false;
        private bool isWeaponPanelOn = false;
        
        public void InputToggleItemPanel(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (isWeaponPanelOn) return;
                
                backgroundPanel.SetActive(true);
                itemPanel.transform.SetAsFirstSibling();
                weaponPanel.transform.SetAsLastSibling();
                isItemPanelOn = true;
            }
            else if (context.canceled)
            {
                backgroundPanel.SetActive(false);
                isItemPanelOn = false;
            }
        }
        
        public void InputToggleWeaponPanel(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (isItemPanelOn) return;
                
                backgroundPanel.SetActive(true);
                weaponPanel.transform.SetAsFirstSibling();
                itemPanel.transform.SetAsLastSibling();
                isWeaponPanelOn = true;
            }
            else if (context.canceled)
            {
                backgroundPanel.SetActive(false);
                isWeaponPanelOn = false;
            }
        }
        
        public void InputScroll(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            Vector2 scrollDelta = context.ReadValue<Vector2>();
    
            if (scrollDelta.y > 0)
            {
                Debug.Log("Scroll Up: Next Item");
                // Add logic for scrolling up
            }
            else if (scrollDelta.y < 0)
            {
                Debug.Log("Scroll Down: Previous Item");
                // Add logic for scrolling down
            }
        }
    }
}