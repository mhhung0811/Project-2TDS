using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : BombBase, IPlayerItem
{
	public int timeBurning;
	public float dps;
	public float damage;
	private SpriteRenderer _spriteRenderer;
	public float yScale = 0.7f;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}
	void Update()
	{
		if (isThrowing)
		{
			Throwing();
		}
	}

	public override void Throw(Vector2 pos)
	{
		isThrowing = true;
		basePos = this.transform.position;
		direction = (pos - basePos);
		verticalSpeed = direction.magnitude * 0.5f;
		float time = 2 * (-verticalSpeed / gravity);
		horizontalSpeed = direction.magnitude / time;
		direction = direction.normalized;
	}

	public override void Explode()
	{
		Vector3 center = transform.position;

		float randomOffset = 0f;


		var layers = new (float radius, int count)[]
		{
		(0.5f, 4),
		(0.75f, 6),
		(1.25f, 10)
		};

		foreach (var layer in layers)
		{
			float radius = layer.radius;
			int count = layer.count;

			for (int i = 0; i < count; i++)
			{
				//randomOffset = Random.Range(-0.1f, 0.1f); 
				float angle = i * Mathf.PI * 2f / count;
				float x = Mathf.Cos(angle) * radius + randomOffset;
				float y = Mathf.Sin(angle) * radius * yScale + randomOffset + 0.75f; // Co trục Y lại

				Vector3 spawnPos = new Vector3(center.x + x, center.y + y, 0);
				//EffectManager.Instance.PlayEffect(EffectType.EfBurning, spawnPos, Quaternion.identity);
				GameObject gobj = EffectManager.Instance.PlayEffect(EffectType.EfBurning, spawnPos, Quaternion.identity);
				EffectBurning effect = gobj.GetComponent<EffectBurning>();
				effect.type = Random.Range(0, 3);
				effect.timeBurning = timeBurning;
			}
		}

		StartCoroutine(StartBurn());
		_spriteRenderer.enabled = false;
	}

	private IEnumerator StartBurn()
	{
		float a = radius;
		float b = radius * yScale;
		int count = (int)(timeBurning / dps);
		Debug.Log("Count: " + count);

		for (int i = 0; i < count; i++)
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
			foreach (Collider2D col in colliders)
			{
				Vector2 delta = col.transform.position - transform.position;
				float xNorm = delta.x / a;
				float yNorm = delta.y / b;

				if (xNorm * xNorm + yNorm * yNorm <= 1f)
				{
					if (col.TryGetComponent<IDamageEffectApplicable>(out var obj))
					{
						obj.Accept(new BurnDamageEffect());
					}
				}
			}

			yield return new WaitForSeconds(dps);
		}

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Vector3 center = transform.position;
		float a = radius;           // trục X
		float b = radius * yScale;    // trục Y

		const int segmentCount = 60;
		Vector3 prevPoint = Vector3.zero;

		for (int i = 0; i <= segmentCount; i++)
		{
			float angle = i * Mathf.PI * 2 / segmentCount;
			float x = Mathf.Cos(angle) * a;
			float y = Mathf.Sin(angle) * b;
			Vector3 currentPoint = new Vector3(x, y, 0) + center;

			if (i > 0)
			{
				Gizmos.DrawLine(prevPoint, currentPoint);
			}

			prevPoint = currentPoint;
		}
	}
	 
	#region IPlayerItem Implementation

	public int manaCost { get; set; }
	public float cooldown { get; set; }

	public void UseItem(Vector2 pos)
	{
		Throw(pos);
	}

	#endregion
}
