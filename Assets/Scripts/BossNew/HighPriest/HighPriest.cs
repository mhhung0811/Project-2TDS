﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriest : MonoBehaviour, IEnemyInteractable, IRoomProp
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
	public Animator gunAnimator;
	public Material damageFlashMAT;
	public Material dissolveMAT;
	public GameObjectFlyweightTypeVector2FloatFuncProvider takeBulletFunc;
	public Transform fireLeft;
	public Transform fireRight;
	public Transform posGun;
	public Vector2Variable playerPos;
	public Vector2Variable posCenter;
	public List<Vector2Variable> posTele;
	public VoidEvent enterDissolve;
	public VoidEvent exitDissolve;
	public VoidEvent FinishInitBossState;

	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rb;
	[HideInInspector]
	public Collider2D col;
	[HideInInspector]
	public Animator animator;
	public GameObject cameraInit;
	#endregion

	#region State Machine
	public HighPriestStateMachine stateMachine;
	public HighPriestInitState initState;
	public HighPriestIdleState idleState;
	public HighPriestAttackState attackState;
	public HighPriestDieState dieState;
	public HighPriestGunState gunState;
	public HighPriestShieldState shieldState;
	public HighPriestDissolveState dissolveState;
	public HighPriestFireState fireState;
	public HighPriestTeleState teleState;
	#endregion
	#region Unity Functions 
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		col = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		stateMachine = new HighPriestStateMachine();
		initState = new HighPriestInitState(this, stateMachine);
		idleState = new HighPriestIdleState(this, stateMachine);
		attackState = new HighPriestAttackState(this, stateMachine);
		dieState = new HighPriestDieState(this, stateMachine);
		gunState = new HighPriestGunState(this, stateMachine);
		shieldState = new HighPriestShieldState(this, stateMachine);
		dissolveState = new HighPriestDissolveState(this, stateMachine);
		fireState = new HighPriestFireState(this, stateMachine);
		teleState = new HighPriestTeleState(this, stateMachine);

		stateMachine.Initialize(initState);
	}
	void Start()
    {
        dissolveMAT.SetFloat("_DissolveAmount", 0f);
		dissolveMAT.SetFloat("_VerticalDissolve", 0f);
	}

    void Update()
    {
		stateMachine.CurrentState.FrameUpdate();
    }

	private void FixedUpdate()
	{
		stateMachine.CurrentState.PhysicsUpdate();
	}
	#endregion

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

	public void SpawnArcBullets(Vector2 pos, Vector2 direction, float totalAngle, int bulletCount)
	{
		float startAngle = -totalAngle / 2f;
		float stepAngle = totalAngle / (bulletCount - 1);

		for (int i = 0; i < bulletCount; i++)
		{
			float angle = startAngle + i * stepAngle;

			takeBulletFunc.GetFunction()((
				FlyweightType.EnemyBullet,
				pos,
				Vector2ToAngle(direction) + angle
			));
		}
	}

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

	public void PlayFlashWhite()
	{
		StartCoroutine(FlashWhite());
	}
	private IEnumerator FlashWhite()
	{
		spriteRenderer.material = damageFlashMAT;
		damageFlashMAT.SetFloat("_FlashAmount", 1f);
		yield return new WaitForSeconds(0.05f);
		damageFlashMAT.SetFloat("_FlashAmount", 0f);
	}

	public void Move()
	{
		transform.position = Vector2.MoveTowards(
			transform.position,
			playerPos.CurrentValue,
			moveSpeed * Time.deltaTime
		);
	}

	public void StartDissolve(float duration)
	{
		StartCoroutine(DissolveEffect(duration));
	}

	public void StartAppear(float duration)
	{
		StartCoroutine(AppearEffect(duration));
	}

	private IEnumerator DissolveEffect(float duration)
	{
		float elapsedTime = 0f;
		dissolveMAT.SetFloat("_DissolveAmount", 0f);
		spriteRenderer.material = dissolveMAT;
		while (elapsedTime < duration)
		{
			float dissolveAmount = Mathf.Lerp(0f, 1f, elapsedTime / duration);
			dissolveMAT.SetFloat("_DissolveAmount", dissolveAmount);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		dissolveMAT.SetFloat("_DissolveAmount", 1f);
	}
	 
	private IEnumerator AppearEffect(float duration)
	{
		float elapsedTime = 0f;
		spriteRenderer.material = dissolveMAT;
		while (elapsedTime < duration)
		{
			float dissolveAmount = Mathf.Lerp(1f, 0f, elapsedTime / duration);
			dissolveMAT.SetFloat("_DissolveAmount", dissolveAmount);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		dissolveMAT.SetFloat("_DissolveAmount", 0f);
	}

	public void StartVerDissolve(float duration)
	{
		StartCoroutine(VerDissolveEffect(duration));
	}

	public void StartVerAppear(float duration)
	{
		StartCoroutine(VerAppearEffect(duration));
	}

	private IEnumerator VerDissolveEffect(float duration)
	{
		float elapsedTime = 0f;
		dissolveMAT.SetFloat("_DissolveAmount", 0f);
		spriteRenderer.material = dissolveMAT;
		while (elapsedTime < duration)
		{
			float dissolveAmount = Mathf.Lerp(0f, 1f, elapsedTime / duration);
			dissolveMAT.SetFloat("_VerticalDissolve", dissolveAmount);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		dissolveMAT.SetFloat("_VerticalDissolve", 1f);
	}

	private IEnumerator VerAppearEffect(float duration)
	{
		float elapsedTime = 0f;
		spriteRenderer.material = dissolveMAT;
		while (elapsedTime < duration)
		{
			float dissolveAmount = Mathf.Lerp(1f, 0f, elapsedTime / duration);
			dissolveMAT.SetFloat("_VerticalDissolve", dissolveAmount);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		dissolveMAT.SetFloat("_VerticalDissolve", 0f);
	}

	public void OnRoomEntry()
	{
		currentHealth.CurrentValue = maxHealth.CurrentValue;
		StopAllCoroutines();
		stateMachine.ChangeState(initState);
	}

	public void OnRoomRefresh()
	{
		gameObject.SetActive(true);
	}

	#region Unity Methods Debug
	[ContextMenu("Init State")]
	public void DebugInitState()
	{
		Debug.Log("Chuyển sang trạng thái Init");
		stateMachine.ChangeState(initState);
	}

	[ContextMenu("Idle State")]
	public void DebugIdleState()
	{
		Debug.Log("Chuyển sang trạng thái Idle");
		stateMachine.ChangeState(idleState);
	}

	[ContextMenu("Attack State")]
	public void DebugAttackState()
	{
		Debug.Log("Chuyển sang trạng thái Idle");
		stateMachine.ChangeState(attackState);
	}

	[ContextMenu("Shield State")]
	public void DebugMoveState()
	{
		Debug.Log("Chuyển sang trạng thái Shield");
		stateMachine.ChangeState(shieldState);
	}

	[ContextMenu("Gun State")]
	public void DebugJumpState()
	{
		Debug.Log("Chuyển sang trạng thái Gun");
		stateMachine.ChangeState(gunState);
	}


	[ContextMenu("Die State")]
	public void DebugDieState()
	{
		Debug.Log("Chuyển sang trạng thái Die");
		stateMachine.ChangeState(dieState);
	}

	[ContextMenu("Dissolve State")]
	public void DebugDissolveState()
	{
		Debug.Log("Chuyển sang trạng thái Dissolve");
		stateMachine.ChangeState(dissolveState);
	}

	[ContextMenu("Fire State")]
	public void DebugFireState()
	{
		Debug.Log("Chuyển sang trạng thái Fire");
		stateMachine.ChangeState(fireState);
	}
	#endregion

}
