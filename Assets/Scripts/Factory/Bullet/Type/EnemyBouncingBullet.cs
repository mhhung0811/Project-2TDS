using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBouncingBullet : Projectile
{
	private bool hasBounced = false;
	public LayerMask wallLayer; // Gán trong Inspector
	public GameObject trail;

	public override void OnEnable()
	{
		base.OnEnable();
		hasBounced = false;
		StartCoroutine(renderTrail());
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		// Xử lý trúng player
		IPlayerInteractable player = collision.GetComponent<IPlayerInteractable>();
		if (player != null && player.IsPlayerInteractable)
		{
			EffectManager.Instance.PlayEffect(EffectType.EfBulletCollide, transform.position, Quaternion.identity);
			settings.flyweightFunc.GetFunction()(this);
			player.OnPlayerBulletHit();
			return;
		}

		// Xử lý trúng tường
		if (collision.CompareTag("Wall"))
		{
			if (hasBounced)
			{
				// Nếu đã bật 1 lần → hủy đạn
				EffectManager.Instance.PlayEffect(EffectType.EfBulletCollide, transform.position, Quaternion.identity);
				settings.flyweightFunc.GetFunction()(this);
				return;
			}

			// Raycast để lấy normal mặt tường
			Vector2 rayDirection = _rb.velocity.normalized;
			Vector2 rayOrigin = (Vector2)transform.position - rayDirection * 0.1f;

			Debug.DrawRay(rayOrigin, rayDirection * 1f, Color.green, 1f); // Vẽ ray trong scene

			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, 1f, wallLayer);
			if (hit.collider != null)
			{
				Vector2 normal = hit.normal;
				Vector2 reflectedDirection = Vector2.Reflect(rayDirection, normal).normalized;

				_rb.velocity = reflectedDirection * settings.speed;
				transform.right = reflectedDirection; // Nếu sprite xoay theo hướng bay
				hasBounced = true;
			}
			else
			{
				hasBounced = true;
			}
		}
	}

	private IEnumerator renderTrail()
	{
		trail = this.transform.Find("Trail").gameObject;
		if(trail != null)
		{
			trail.SetActive(false);
			yield return new WaitForSeconds(0.1f);
			trail.SetActive(true);
		}
	}
}
