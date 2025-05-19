using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{

	public float gravity;
	private float verticalSpeed;
	private float horizontalSpeed;
	private float height;
	private Vector2 basePos;
	private Vector2 direction;
	public Vector2 targetPos;

	private bool isThrowing = false;
	void Start()
	{
		StartCoroutine(OnExplode());
	}

	// Update is called once per frame
	void Update()
	{
		if (isThrowing)
		{
			verticalSpeed = verticalSpeed + gravity * Time.deltaTime;
			height = height + verticalSpeed * Time.deltaTime;

			basePos += direction * horizontalSpeed * Time.deltaTime;

			this.transform.position = new Vector3(basePos.x, basePos.y + height, 0.0f);

			if (height < 0.0f)
			{
				isThrowing = false;
				height = 0.0f;
				basePos = this.transform.position;
				Explode();
			}
		}
	}

	public void Throw(Vector2 pos)
	{
		isThrowing = true;
		basePos = this.transform.position;
		direction = (pos - basePos);
		verticalSpeed = direction.magnitude * 0.5f;
		float time = 2 * (-verticalSpeed / gravity);
		horizontalSpeed = direction.magnitude / time;
		direction = direction.normalized;
	}

	public void Explode()
	{
		Vector3 center = transform.position;

		float randomOffset = 0f;

		// Hệ số co trục Y để tạo elip
		float yScale = 0.6f;

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
				float angle = i * Mathf.PI * 2f / count - Mathf.PI/2;
				float x = Mathf.Cos(angle) * radius + randomOffset;
				float y = Mathf.Sin(angle) * radius * yScale + randomOffset; // Co trục Y lại

				Vector3 spawnPos = new Vector3(center.x + x, center.y + y, 0);
				//EffectManager.Instance.PlayEffect(EffectType.EfBurning, spawnPos, Quaternion.identity);
				GameObject gobj = EffectManager.Instance.PlayEffect(EffectType.EfBurning, spawnPos, Quaternion.identity);
				EffectBurning effect = gobj.GetComponent<EffectBurning>();
				effect.type = Random.Range(0, 3);
			}
		}

		Destroy(this.gameObject);
	}


	private IEnumerator OnExplode()
	{
		yield return new WaitForSeconds(1f);
		Throw(targetPos);
	}
}
