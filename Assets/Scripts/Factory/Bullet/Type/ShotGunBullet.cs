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
				settings.flyweightFunc.GetFunction()(this);
			}
			return;
		}

		if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Boss"))
		{
			EffectManager.Instance.PlayEffect(EffectType.EfBulletCollide, transform.position, Quaternion.identity);
			settings.flyweightFunc.GetFunction()(this);
			return;
		}
	}
}
