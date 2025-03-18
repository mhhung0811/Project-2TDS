using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIRoot : MonoBehaviour
{
    public GameObject MenuMinimap;
    public GameObject Minimap;
    public GameObject MenuGame;
    public GameObject MenuWin;
    public TextMeshProUGUI textTimeWin;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InputOpenMenuMinimap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.Instance.isHoldButtonTab = true;
            MenuMinimap.SetActive(true);
            Minimap.SetActive(false);
        }
        else
        {
            GameManager.Instance.isHoldButtonTab = false;
            MenuMinimap.SetActive(false);
            Minimap.SetActive(true);
        }
    }

    public void OpenMenuGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MenuGame.SetActive(true);
            GameManager.Instance.PauseGame();
        }
    }

    public void OnPlayerDie()
    {
        GameManager.Instance.PauseGame();
    }

    public void OnBossDied()
    {
        GameManager.Instance.PauseGame();
        SoundManager.Instance.StopAllSounds();
    }

    public void OnWinGame()
    {
        MenuWin.SetActive(true);
        if(textTimeWin.enabled == false)
        {
            textTimeWin.enabled = true;
		}
        GameManager.Instance.PauseGame();
	}
}
