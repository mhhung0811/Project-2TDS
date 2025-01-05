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
			player.OnPlayerBulletHit();
			if(player.IsPlayerInteractable)
			{
				settings.flyweightEvent.Raise(this);
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
