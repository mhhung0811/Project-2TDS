using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Lich1 : MonoBehaviour, IEnemyInteractable
{
	#region Boss Properties
	public FloatVariable maxHealth;
	public FloatVariable currentHealth;
	public float moveSpeed;
	#endregion

	#region Interface variables
	public bool IsEnemyInteractable { get; set; } = true;
	#endregion

	#region Boss Components
	public Material damageFlashMAT;
	private Material damageFlashMATRunTime;
	public GameObjectFlyweightTypeVector2FloatFuncProvider takeBulletFunc;
	public Transform posGun;
	public Transform posGunInit;
	public Vector2Variable playerPos;
	public Vector2Variable posCenter;

	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rb;
	[HideInInspector]
	public Collider2D col;
	[HideInInspector]
	public Animator animator;
	#endregion

	#region State Machine
	public Lich1StateMachine stateMachine;
	public Lich1InitState initState;
	public Lich1IdleState idleState;
	public Lich1AttackAOEState attackAOEState;
	public Lich1GunState gunState;
	public Lich1ExplodeState explodeState;
	public Lich1DieState dieState;
	#endregion

	#region Unity Functions 
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		col = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		damageFlashMATRunTime = new Material(damageFlashMAT);

		stateMachine = new Lich1StateMachine();
		initState = new Lich1InitState(this, stateMachine);
		idleState = new Lich1IdleState(this, stateMachine);
		attackAOEState = new Lich1AttackAOEState(this, stateMachine);
		gunState = new Lich1GunState(this, stateMachine);
		explodeState = new Lich1ExplodeState(this, stateMachine);
		dieState = new Lich1DieState(this, stateMachine);

		stateMachine.Initialize(initState);
	}

	private void Start()
	{
	}

	private void Update()
	{
		stateMachine.CurrentState.FrameUpdate();
	}

	private void FixedUpdate()
	{
		stateMachine.CurrentState.PhysicsUpdate();
	}
	#endregion

	public void OnEnemyBulletHit(float damge)
	{
		if (stateMachine.CurrentState == initState || stateMachine.CurrentState == dieState || !IsEnemyInteractable) return;

		currentHealth.CurrentValue = currentHealth.CurrentValue - damge;
		if (currentHealth.CurrentValue <= 0)
		{
			spriteRenderer.material = damageFlashMAT; // Reset material to avoid flashing after death
			damageFlashMAT.SetFloat("_FlashAmount", 0f);
			stateMachine.ChangeState(dieState);
			return;
		}
		StartCoroutine(FlashWhite());
	}

	private IEnumerator FlashWhite()
	{
		spriteRenderer.material = damageFlashMATRunTime;
		damageFlashMATRunTime.SetFloat("_FlashAmount", 0.5f);
		yield return new WaitForSeconds(0.05f);
		damageFlashMATRunTime.SetFloat("_FlashAmount", 0f);
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

	public Vector2 AngleToVector2(float angleInDegrees)
	{
		float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
		return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
	}

	public void SpawnArcBullets(Vector2 pos, Vector2 direction, float totalAngle, int bulletCount, FlyweightType type = FlyweightType.LichGunBullet)
	{
		float startAngle = -totalAngle / 2f;
		float stepAngle = totalAngle / (bulletCount);

		for (int i = 0; i < bulletCount; i++)
		{
			float angle = startAngle + i * stepAngle;

			takeBulletFunc.GetFunction()((
				type,
				pos,
				Vector2ToAngle(direction) + angle
			));
		}
	}
}
