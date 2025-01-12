using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Projectile
{
	public void OnTriggerEnter2D(Collider2D collision)
	{
		IPlayerInteractable player = collision.gameObject.GetComponent<IPlayerInteractable>();
		if (player != null)
		{
			if(player.IsPlayerInteractable)
			{
				settings.flyweightEvent.Raise(this);
				player.OnPlayerBulletHit();
			}
			return;
		}

		if (collision.gameObject.CompareTag("Wall"))
		{
			settings.flyweightEvent.Raise(this);
			return;
		}
	}
}
