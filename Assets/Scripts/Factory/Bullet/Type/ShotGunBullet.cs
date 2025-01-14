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
				EffectManager.Instance.PlayEffect(EffectType.EfBulletCollide, transform.position, Quaternion.identity);
				enemy.OnEnemyBulletHit(settings.damage);
				settings.flyweightEvent.Raise(this);
			}
			return;
		}

		if (collision.gameObject.CompareTag("Wall"))
		{
			EffectManager.Instance.PlayEffect(EffectType.EfBulletCollide, transform.position, Quaternion.identity);
			settings.flyweightEvent.Raise(this);
			return;
		}
	}
}
