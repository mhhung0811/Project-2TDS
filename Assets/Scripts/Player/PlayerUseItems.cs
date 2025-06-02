using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseItems : MonoBehaviour
{
    public GameObject grenade;
    public GameObject molotov;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InputUseGrenade(InputAction.CallbackContext context)
    {
		if (GameManager.Instance.isOpenUI)
		{
			return;
		}

        if(context.performed)
		{
			UseGrenade();
		}
	}

	public void InputUseMolotov(InputAction.CallbackContext context)
	{
		if (GameManager.Instance.isOpenUI)
		{
			return;
		}

		if (context.performed)
		{
			UseMolotov();
		}
	}


	public void UseGrenade()
    {
		GameObject grenadeInstance = Instantiate(grenade, transform.position, Quaternion.identity);
		Grenade grenadeScript = grenadeInstance.GetComponent<Grenade>();
        Vector2 targetPos = InputUtils.GetMouseWorldPosition(Camera.main);
		grenadeScript.Throw(targetPos);
	}

	public void UseMolotov()
	{
		GameObject molotovInstance = Instantiate(molotov, transform.position, Quaternion.identity);
		Molotov molotovScript = molotovInstance.GetComponent<Molotov>();
		Vector2 targetPos = InputUtils.GetMouseWorldPosition(Camera.main);
		molotovScript.Throw(targetPos);
	}
}
