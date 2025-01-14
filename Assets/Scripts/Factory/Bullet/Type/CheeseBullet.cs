using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBullet : Projectile
{
	public float acceleration;
	public float maxSpeed;
	private float currentSpeed;

	public override void OnEnable()
	{
		base.OnEnable();
		currentSpeed = settings.speed;
	}
	private void Update()
	{
		Move();
	}
	public void Move()
	{
		currentSpeed += acceleration * Time.deltaTime;
		if (currentSpeed > maxSpeed)
		{
			currentSpeed = maxSpeed;
		}
		_rb.velocity = currentSpeed * _rb.velocity.normalized;
	}
	public void OnTriggerEnter2D(Collider2D collision)
	{
		IPlayerInteractable player = collision.gameObject.GetComponent<IPlayerInteractable>();
		if (player != null)
		{
			if (player.IsPlayerInteractable)
			{
				EffectManager.Instance.PlayEffect(EffectType.EfBulletCollide, transform.position, Quaternion.identity);
				settings.flyweightEvent.Raise(this);
				player.OnPlayerBulletHit();
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
