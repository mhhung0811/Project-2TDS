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
				EffectManager.Instance.PlayEffect(EffectType.EfBulletCollide, transform.position, Quaternion.identity);
				settings.flyweightFunc.GetFunction()(this);
				player.OnPlayerBulletHit();
			}
			return;
		}

		if (collision.gameObject.CompareTag("Wall"))
		{
			EffectManager.Instance.PlayEffect(EffectType.EfBulletCollide, transform.position, Quaternion.identity);
			settings.flyweightFunc.GetFunction()(this);
			return;
		}
	}
}
