using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunnut : Enemy
{
	private float timeToAttack = 2f;

	public PatrolArea patrolArea;
	[HideInInspector]
	public Unit unit;
	public GameObjectFlyweightTypeVector2FloatFuncProvider takeBulletFunc;
	public Material damageFlashMAT;
	private Material damageFlashMATRunTime;
	private SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();
	// State Machine Variables
	public GunnutMoveState moveState { get; set; }
	public GunnutDieState dieState { get; set; }
	public GunnutAttackState attackState { get; set; }
	public GunnutPatrolState patrolState { get; set; }

	private bool isDead = false;
	private void Awake()
	{
		unit = GetComponent<Unit>();
		animator = GetComponent<Animator>();
		RB = GetComponent<Rigidbody2D>();
		damageFlashMATRunTime = new Material(damageFlashMAT);

		StateMachine = new EnemyStateMachine();
		moveState = new GunnutMoveState(this, StateMachine);
		dieState = new GunnutDieState(this, StateMachine);
		attackState = new GunnutAttackState(this, StateMachine);
		patrolState = new GunnutPatrolState(this, StateMachine);
	}
	private void Start()
	{
		IsEnemyInteractable = true;
		StateMachine.Initialize(patrolState);
	}
	private void Update()
	{
		StateMachine.CurrentState.FrameUpdate();
	}
	private void FixedUpdate()
	{
		StateMachine.CurrentState.PhysicsUpdate();
	}

	private void OnEnable()
	{
		if(isDead)
		{
			CurrentHealth = MaxHealth;
			isDead = false;
			StateMachine.ChangeState(patrolState);
		}
	}

	private void OnDisable()
	{
		isDead = true;
	}

	public float GetTimeToAttack()
	{
		return timeToAttack;
	}

	public void SpawnArcBullets(Vector2 pos, Vector2 direction, float totalAngle, int bulletCount)
	{
		float startAngle = -totalAngle / 2f;
		float stepAngle = totalAngle / (bulletCount - 1);

		for (int i = 0; i < bulletCount; i++)
		{
			float angle = startAngle + i * stepAngle;

			takeBulletFunc.GetFunction()((
				FlyweightType.GunnutBullet,
				pos,
				Vector2ToAngle(direction) + angle
			));
		}
	}

	public void SpawnHeadArrowBullet(Vector2 pos, Vector2 direction)
	{
		float angle = Vector2ToAngle(direction);

		takeBulletFunc.GetFunction()((
			FlyweightType.GunnutBullet,
			pos,
			angle
		));

		// left
		float angleLeft = angle - 45f;
		Vector2 dirLeft = AngleToVector2(angleLeft);

		for (int i = 1; i <= 1; i++)
		{
			takeBulletFunc.GetFunction()((
				FlyweightType.GunnutBullet,
				pos - dirLeft * i * 0.4f,
				angle
			));
		}

		// right
		float angleRight = angle + 45f;
		Vector2 dirRight = AngleToVector2(angleRight);
		for (int i = 1; i <= 1; i++)
		{
			takeBulletFunc.GetFunction()((
				FlyweightType.GunnutBullet,
				pos - dirRight * i * 0.4f,
				angle
			));
		}
	}

	public override void OnEnemyBulletHit(float damage)
	{
		CurrentHealth -= (int)damage;

		if (CurrentHealth <= 0)
		{
			StateMachine.ChangeState(dieState);
			// ResetMAT
			damageFlashMATRunTime.SetFloat("_FlashAmount", 0f);
			return;
		}
		StartCoroutine(FlashWhite());
	}

	private IEnumerator FlashWhite()
	{
		SpriteRenderer.material = damageFlashMATRunTime;
		damageFlashMATRunTime.SetFloat("_FlashAmount", 0.5f);
		yield return new WaitForSeconds(0.05f);
		damageFlashMATRunTime.SetFloat("_FlashAmount", 0f);
	}
}
