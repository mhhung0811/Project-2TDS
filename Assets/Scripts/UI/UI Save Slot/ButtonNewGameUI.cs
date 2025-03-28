using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNewGameUI : MonoBehaviour
{
    public GameObject loadingPanel;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void OnClick()
	{
		loadingPanel.SetActive(true);
		GameManager.Instance.PlayNewGame();
	}
}
