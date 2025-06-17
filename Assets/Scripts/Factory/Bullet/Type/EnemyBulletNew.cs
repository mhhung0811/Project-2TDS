using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletNew : Projectile
{
	private float xDirection;
	private float yDirection;
	public void StartMove()
	{
		_rb.velocity = settings.speed * new Vector2(xDirection, yDirection);
	}

	public void SetRotate(float angle)
	{
		this.transform.rotation = Quaternion.Euler(0, 0, angle);
		xDirection = Mathf.Cos(angle * Mathf.Deg2Rad);
		yDirection = Mathf.Sin(angle * Mathf.Deg2Rad);
	}
	public override void OnEnable()
	{
		float rotation = transform.rotation.eulerAngles.z;
		xDirection = Mathf.Cos(rotation * Mathf.Deg2Rad);
		yDirection = Mathf.Sin(rotation * Mathf.Deg2Rad);
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		IPlayerInteractable player = collision.gameObject.GetComponent<IPlayerInteractable>();
		if (player != null)
		{
			if (player.IsPlayerInteractable)
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
