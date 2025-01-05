using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTailWhip : Projectile
{
	public float destroyTime = 4f;
	public float canDestroyTime = 2f;
	private bool canDestroyWithWall = false;
	private float angleSpeed;
	private float angleStart;
	private float radius;
	public Vector2Variable BossPos;
	public override void Awake()
	{
		base.Awake();
	}

	void Start()
    {
    }

    void Update()
    {
    }

	void FixedUpdate()
	{
		MoveCircle();
	}

	public void MoveCircle()
	{
		if (!canDestroyWithWall)
		{
			angleStart += angleSpeed * Time.fixedDeltaTime;

			angleStart %= (2 * Mathf.PI);
			float x = Mathf.Cos(angleStart);
			float y = Mathf.Sin(angleStart);

			transform.position = new Vector2(x, y) * radius + BossPos.CurrentValue;
		}
	}

	public override void OnEnable()
	{
		Init();
		StartCoroutine(SetCanDestroy());
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}

	public void Init()
	{
		canDestroyWithWall = false;
		Vector2 distance = (Vector2)transform.position - BossPos.CurrentValue;
		radius = distance.magnitude;
		angleSpeed = settings.speed;
		angleStart = Mathf.Atan2(distance.y, distance.x);
	}


	public IEnumerator SetCanDestroy()
	{
		yield return new WaitForSeconds(canDestroyTime);
		canDestroyWithWall = true;
		StartCoroutine(DestroyOvertime());

		float vx = -angleSpeed * radius * Mathf.Sin(angleStart);
		float vy = angleSpeed * radius * Mathf.Cos(angleStart);
		_rb.velocity = new Vector2(vx, vy);
	}

	public IEnumerator DestroyOvertime()
	{
		yield return new WaitForSeconds(destroyTime);
		settings.flyweightEvent.Raise(this);
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		IEnemyInteractable enemy = collision.gameObject.GetComponent<IEnemyInteractable>();
		if (enemy != null)
		{
			enemy.OnEnemyBulletHit(settings.damage);
			settings.flyweightEvent.Raise(this);
			StopAllCoroutines();
			return;
		}

		if (collision.gameObject.CompareTag("Wall") && canDestroyWithWall)
		{
			settings.flyweightEvent.Raise(this);
			StopAllCoroutines();
			return;
		}
	}
}
