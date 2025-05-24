using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShee : Enemy
{
	public SkillBombShee SkillBombShee { get; set; }

	public bool canScreech = false;
	public float cooldownScreech;
	public float timeScreech;

	public float targetRange;
	public Enemy enemyTarget;
	public LayerMask targetLayer;

	public float protectRange;


	#region State Variables
	public BombSheeIdleState IdleState { get; set; }
	public BombSheeMoveState MoveState { get; set; }
	public BombSheeDieState DieState { get; set; }
	public BombSheeAttackState AttackState { get; set; }
	public BombSheeBulletState BulletState { get; set; }
	public BombSheeScreechState ScreechState { get; set; }
	#endregion

	private void Awake()
	{
		StateMachine = new EnemyStateMachine();
		IdleState = new BombSheeIdleState(this, StateMachine);
		MoveState = new BombSheeMoveState(this, StateMachine);
		DieState = new BombSheeDieState(this, StateMachine);
		AttackState = new BombSheeAttackState(this, StateMachine);
		BulletState = new BombSheeBulletState(this, StateMachine);
		ScreechState = new BombSheeScreechState(this, StateMachine);

		SkillBombShee = GetComponentInChildren<SkillBombShee>();
	}

	private void Start()
	{
		CurrentHealth = MaxHealth;
		IsEnemyInteractable = true;
		RB = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		canScreech = true;
		enemyTarget = CheckTargetEnemy();
		Debug.Log(enemyTarget.transform.position);
		StateMachine.Initialize(MoveState);
	}

	private void Update()
	{
		UpdateTarget();
		StateMachine.CurrentState.FrameUpdate();
	}

	private void FixedUpdate()
	{
		StateMachine.CurrentState.PhysicsUpdate();
	}

	#region Base Functions
	public void CallbackEndAniScreech()
	{
		StateMachine.ChangeState(IdleState);
	}

	public void CallbackEndAniAttack()
	{
		Debug.Log("End Attack Animation");
		StateMachine.ChangeState(BulletState);
	}

	public void CallbackEndAniDamaged()
	{
		animator.SetBool("IsDamaged", false);
	}

	public void Screech()
	{
		if(canScreech)
		{
			canScreech = false;
			StateMachine.ChangeState(ScreechState);
		}
	}

	private void UpdateTarget()
	{
		if(StateMachine.CurrentState != MoveState && StateMachine.CurrentState != IdleState)
			return;

		if (enemyTarget == null)
		{
			Debug.Log("Target is null When Init");
			StateMachine.ChangeState(AttackState);
		}
		else if(enemyTarget.gameObject.activeSelf == false)
		{
			enemyTarget = CheckTargetEnemy();
			if(enemyTarget == null)
			{
				Debug.Log("Target is null When Update");
				StateMachine.ChangeState(AttackState);
			}
		}
	}

	public bool OnProtectRange()
	{
		if (enemyTarget == null)
		{
			Debug.LogError("Target is null When Check Protect Range");
			return false;
		}

		return Vector2.Distance(transform.position, enemyTarget.transform.position) <= protectRange;
	}

	public Enemy CheckTargetEnemy()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetRange, targetLayer);
		Enemy target = null;
		float distance = Mathf.Infinity;
		foreach (Collider2D hit in hits)
		{
			if(hit.gameObject == this.gameObject)
				continue;

			if (hit.TryGetComponent<Enemy>(out Enemy enemy))
			{
				float currentDistance = Vector2.Distance(transform.position, enemy.transform.position);
				if (currentDistance < distance)
				{
					distance = currentDistance;
					target = enemy;
				}
			}
		}

		return target;
	}

	public override void OnExplode(float damage)
	{
		StopAllCoroutines();
		
		StartCoroutine(RecoverDrag());
	}

	public IEnumerator RecoverDrag()
	{
		yield return new WaitForSeconds(0.5f);
		RB.drag = 0;
	}
	#endregion

	public override void OnEnemyBulletHit(float damage)
	{
		Debug.Log("BombShee: OnEnemyBulletHit with damage: " + damage);
		CurrentHealth -= (int)damage;
		animator.SetBool("IsDamaged", true);

		if (CurrentHealth <= 0)
		{
			StateMachine.ChangeState(DieState);
			return;
		}
	}

	public override void OnDrawGizmos()
	{
		// Draw target range
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, targetRange);

		// Draw protect range
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, protectRange);
	}
}
