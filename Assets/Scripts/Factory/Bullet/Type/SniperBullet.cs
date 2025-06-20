using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : Projectile
{
	private TrailRenderer _trailRenderer;
	public override void Awake()
	{
		base.Awake();
		_trailRenderer = GetComponentInChildren<TrailRenderer>();
	}

	public override void OnEnable()
	{
		base.OnEnable();
		if (_trailRenderer != null)
		{
			_trailRenderer.Clear();
		}
	}

	public override void OnDisable()
	{
		base.OnDisable();
		_trailRenderer.Clear();
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		IEnemyInteractable enemy = collision.gameObject.GetComponent<IEnemyInteractable>();
		if (enemy != null)
		{
			if(enemy.IsEnemyInteractable)
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
