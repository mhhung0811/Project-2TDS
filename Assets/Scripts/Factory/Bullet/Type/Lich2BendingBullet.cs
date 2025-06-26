using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2BendingBullet : Projectile
{
	private float xDirection;
	private float yDirection;
	private Vector2 baseDirection;
	private Vector2 perpendicularDirection;
	private Vector3 startPosition;
	private float time;

	public float amplitude = 0.5f;   // Biên độ sóng
	public float frequency = 2f;     // Tần số sóng
	public float delayMove = 0f;
	private float delaytimer = 0f;

	public override void OnEnable()
	{
		float rotation = transform.rotation.eulerAngles.z;
		xDirection = Mathf.Cos(rotation * Mathf.Deg2Rad);
		yDirection = Mathf.Sin(rotation * Mathf.Deg2Rad);

		baseDirection = new Vector2(xDirection, yDirection).normalized;
		perpendicularDirection = new Vector2(-yDirection, xDirection).normalized;

		startPosition = transform.position;
		time = 0f;
		delaytimer = 0f;
	}

	public override void OnDisable()
	{
		base.OnDisable();

	}

	private void Update()
	{
		if(delaytimer < delayMove)
		{
			delaytimer += Time.deltaTime;
			return;
		}
		time += Time.deltaTime;

		// Tính vị trí theo hướng gốc
		Vector2 straightPos = baseDirection * settings.speed * time;

		// Dao động vuông góc (hình sin)
		float offset = Mathf.Sin(time * frequency) * amplitude;
		Vector2 waveOffset = perpendicularDirection * offset;

		// Cập nhật vị trí viên đạn
		transform.position = startPosition + (Vector3)(straightPos + waveOffset);
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
