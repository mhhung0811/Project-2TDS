using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Lich2HeadWireBullet : Projectile
{
	public GameObjectFlyweightTypeVector2FloatFuncProvider takeBulletFunc;
	public float spacing = 0.5f; // Distance between bullets
	private Vector2 moveDir;
	private Vector2 perpendicular;
	private Vector2 posStart;
	private List<Lich2WireBullet> bullets = new List<Lich2WireBullet>();
	private bool stopSpawn = false;

	public float offset = 0.5f;
	private int dirOffset = 1;

	public override void OnEnable()
	{
		base.OnEnable();
		moveDir = _rb.velocity.normalized;
		perpendicular = new Vector2(-moveDir.y, moveDir.x).normalized; // Perpendicular direction to the movement
		posStart = transform.position;
		_rb.velocity = Vector2.zero; // Stop the initial velocity

		// Spawn the first bullet
		bullets.Clear();

		stopSpawn = false;
	}

	private void Update()
	{
		if (stopSpawn)
		{
			return;
		}

		if (bullets.Count == 0)
		{
			var bullet = SpawnWireBullet(posStart, moveDir);
			bullets.Add(bullet);
		}
		else
		{
			//float distance =Mathf.Abs(Vector2.Dot((Vector2)bullets[bullets.Count - 1].transform.position - posStart, moveDir));
			float distance = Vector2.Distance(bullets[bullets.Count - 1].transform.position, posStart);
			if (distance >= spacing)
			{
				int countSpawn = Mathf.FloorToInt(distance / spacing);
				Vector2 lastSpawnPos = bullets[bullets.Count - 1].transform.position;
				for (int i = 0; i < countSpawn; i++)
				{
					Vector2 spawnPos = lastSpawnPos - moveDir.normalized * spacing;
					var bullet = SpawnWireBullet(spawnPos, moveDir);
					bullet.transform.SetParent(transform);
					bullet.GetComponent<Rigidbody2D>().isKinematic = true;
					bullets.Add(bullet);
					lastSpawnPos = spawnPos;
				}
			}
		}

		UpdatePosAllBullet();
	}

	private void UpdatePosAllBullet()
	{
		transform.position = (Vector2)transform.position + moveDir * Time.deltaTime * settings.speed;

		for (int i = 0; i < bullets.Count;i++)
		{
			bullets[i].transform.position = (Vector2)transform.position - moveDir * spacing * i;
		}
	}

	private Lich2WireBullet SpawnWireBullet(Vector2 pos, Vector2 direction)
	{
		var obj = takeBulletFunc.GetFunction()((
			FlyweightType.Lich2WireBullet,
			pos,
			Vector2ToAngle(direction)
		));

		return obj.GetComponent<Lich2WireBullet>();
	}

	public float Vector2ToAngle(Vector2 direction)
	{
		float angleInRadians = Mathf.Atan2(direction.y, direction.x);

		float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

		if (angleInDegrees < 0)
		{
			angleInDegrees += 360f;
		}

		return angleInDegrees;
	}

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
			_rb.velocity = Vector2.zero;

			stopSpawn = true;
			for (int i =0; i < bullets.Count; i++)
			{
				bullets[i].CoroutineClear(3f);
			}
			StartCoroutine(AutoClear(3f));
			return;
		}
	}

	private IEnumerator AutoClear(float time)
	{
		yield return new WaitForSeconds(time);
		settings.flyweightFunc.GetFunction()(this);
	}
}
