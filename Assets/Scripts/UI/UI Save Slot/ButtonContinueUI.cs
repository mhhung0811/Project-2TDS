using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonContinueUI : MonoBehaviour
{
    public GameObject mainText;
    public GameObject loadingPanel;
    void Start()
    {
        OnChangeSaveSlot();   
    }

    void Update()
    {
        
    }

    public void OnChangeSaveSlot()
    {
        bool exist = SaveGameManager.Instance.CheckCurrentSaveSlot();
        if (exist)
        {
            this.gameObject.GetComponent<Button>().interactable = true;
			mainText.SetActive(true);
		}
        else
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            mainText.SetActive(false);
        }
	}

    public void OnClick()
	{
        loadingPanel.SetActive(true);
		GameManager.Instance.PlayContinueGame();
	}
}
