using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCell : MonoBehaviour, IInteractable
{
	public Rigidbody2D rb;
	public bool isInteractable { get; set; }

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		StartCoroutine(StateInit());
	}

	public void Interact(GameObject go)
	{
		var player = go.GetComponent<Player>();
		if (player != null)
		{
			PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
			if(playerInventory != null)
			{
				GunBase gun = playerInventory.GetHoldingGun();
				if (gun.isInfiniteAmmo)
				{
					gun.totalAmmo = -1;
					playerInventory.playerTotalAmmo.CurrentValue = gun.totalAmmo;
				}
				else
				{
					gun.totalAmmo += gun.maxAmmoPerMag * 5;
					playerInventory.playerTotalAmmo.CurrentValue = gun.totalAmmo;
				}
			}
			Destroy(gameObject);
		}
	}

	public IEnumerator StateInit()
	{
		rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
		yield return new WaitForSeconds(1f);
		rb.gravityScale = 0;
		rb.velocity = Vector2.zero;
	}
}
