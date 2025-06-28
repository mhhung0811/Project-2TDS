using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ToggleTabListUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> tabs;
        [SerializeField] private Button nextTab;
        [SerializeField] private Button previousTab;
        
        private int currentTabIndex = 0;

        private void Awake()
        {
            UpdateTabDisplay();
        }

        public void GoToNextTab()
        {
            if (currentTabIndex < tabs.Count - 1)
            {
                currentTabIndex++;
                UpdateTabDisplay();
            }
        }

        public void GoToPreviousTab()
        {
            if (currentTabIndex > 0)
            {
                currentTabIndex--;
                UpdateTabDisplay();
            }
        }
        
        private void UpdateTabDisplay()
        {
            // Show only the current tab
            for (int i = 0; i < tabs.Count; i++)
            {
                tabs[i].SetActive(i == currentTabIndex);
            }

            // Enable/Disable navigation buttons based on position
            previousTab.gameObject.SetActive(currentTabIndex > 0);
            nextTab.gameObject.SetActive(currentTabIndex < tabs.Count - 1);
        }

    }
}