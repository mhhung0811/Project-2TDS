using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBullet : Projectile
{
	public void OnTriggerEnter2D(Collider2D collision)
	{
		IEnemyInteractable enemy = collision.gameObject.GetComponent<IEnemyInteractable>();
		if (enemy != null)
		{
			if (enemy.IsEnemyInteractable)
			{
				enemy.OnEnemyBulletHit(settings.damage);
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
