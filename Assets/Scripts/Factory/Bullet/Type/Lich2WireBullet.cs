using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2WireBullet : Projectile
{
	private float xDirection;
	private float yDirection;
	private Vector2 baseDirection;
	private Vector2 perpendicularDirection;
	private Vector3 startPosition;
	private float time;

	public float amplitude = 0.5f;   // Biên độ sóng
	public float frequency = 2f;     // Tần số sóng
	private bool startMove = false;


	public override void OnEnable()
	{
		float rotation = transform.rotation.eulerAngles.z;
		xDirection = Mathf.Cos(rotation * Mathf.Deg2Rad);
		yDirection = Mathf.Sin(rotation * Mathf.Deg2Rad);

		baseDirection = new Vector2(xDirection, yDirection).normalized;
		perpendicularDirection = new Vector2(-yDirection, xDirection).normalized;

		time = 0f;
		startMove = false;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		StopAllCoroutines();
	}

	private void Update()
	{
		//if (!startMove)
		//{
		//	return;
		//}

		//time += Time.deltaTime;

		//// Thời gian 1 chu kỳ của sóng sin
		//float period = 1f / frequency;

		//if (time <= period)
		//{
		//	// Dao động sin vuông góc quanh vị trí ban đầu
		//	float offset = Mathf.Sin(time * frequency * 2f * Mathf.PI) * amplitude;
		//	Vector2 waveOffset = perpendicularDirection * offset;

		//	// Cập nhật vị trí viên đạn
		//	transform.position = startPosition + (Vector3)waveOffset;
		//}
		//else
		//{
		//	transform.position = startPosition;
		//	startMove = false; // Dừng chuyển động sau khi hoàn thành 1 chu kỳ
		//}
	}

	//public void StartMove()
	//{
	//	startMove = true;
	//	startPosition = transform.position; // Lưu vị trí ban đầu khi bắt đầu chuyển động
	//}


	public void OnTriggerEnter2D(Collider2D collision)
	{
		IPlayerInteractable player = collision.gameObject.GetComponent<IPlayerInteractable>();
		if (player != null)
		{
			if (player.IsPlayerInteractable)
			{
				EffectManager.Instance.PlayEffect(EffectType.EfBulletCollide, transform.position, Quaternion.identity);
				player.OnPlayerBulletHit();
			}
			return;
		}

		if (collision.gameObject.CompareTag("Wall"))
		{
			EffectManager.Instance.PlayEffect(EffectType.EfBulletCollide, transform.position, Quaternion.identity);
			return;
		}
	}

	public void CoroutineClear(float time)
	{
		StartCoroutine(AutoClean(time));
	}

	private IEnumerator AutoClean(float time)
	{
		yield return new WaitForSeconds(time);
		settings.flyweightFunc.GetFunction()(this);
	}
}
