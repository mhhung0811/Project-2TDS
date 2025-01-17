using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIRoot : MonoBehaviour
{
    public GameObject MenuMinimap;
    public GameObject Minimap;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputOpenMenuMinimap(InputAction.CallbackContext context)
    {
        if(context.performed)
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
}
