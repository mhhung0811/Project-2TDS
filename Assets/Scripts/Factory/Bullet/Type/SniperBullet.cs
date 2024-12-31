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
			enemy.OnEnemyBulletHit(settings.damage);
			settings.flyweightEvent.Raise(this);
			return;
		}

		if (collision.gameObject.CompareTag("Wall"))
		{
			settings.flyweightEvent.Raise(this);
			return;
		}
	}
}
