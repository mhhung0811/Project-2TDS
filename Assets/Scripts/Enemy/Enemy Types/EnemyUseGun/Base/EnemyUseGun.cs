using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUseGun : Enemy
{
	[field: SerializeField] public float AttackDuration { get; set; }
	[field: SerializeField] public float AttackCooldown { get; set; }

	[field: SerializeField] public PatrolArea patrolArea;

	public float attackCooldownTimer;

	private bool isDead = false;

	#region State Machine Variables
	public EnemyIdleState IdleState { get; set; }
	public EnemyChaseState ChaseState { get; set; }
	public EnemyAttackState AttackState { get; set; }
	public EnemyInitState InitState { get; set; }
	public EnemyDieState DieState { get; set; }
	public EnemyHurtState HurtState { get; set; }
	public EnemyPatrolState PatrolState { get; set; }
	public EnemyKnockbackState KnockbackState { get; set; }
	#endregion


	#region Function of Unity
	private void Awake()
	{
		StateMachine = new EnemyStateMachine();
		IdleState = new EnemyIdleState(this, StateMachine);
		ChaseState = new EnemyChaseState(this, StateMachine);
		AttackState = new EnemyAttackState(this, StateMachine);
		InitState = new EnemyInitState(this, StateMachine);
		DieState = new EnemyDieState(this, StateMachine);
		HurtState = new EnemyHurtState(this, StateMachine);
		PatrolState = new EnemyPatrolState(this, StateMachine);
		KnockbackState = new EnemyKnockbackState(this, StateMachine);
	}

	private void Start()
	{
		CurrentHealth = MaxHealth;
		IsEnemyInteractable = true;
		RB = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		StateMachine.Initialize(PatrolState);
	}

	private void Update()
	{
		StateMachine.CurrentState.FrameUpdate();
		CheckForFlip();
		UpdateAttackCoolDown();
	}

	private void FixedUpdate()
	{
		StateMachine.CurrentState.PhysicsUpdate();
	}
	#endregion

	public virtual void Attack()
	{

	}

	public virtual void Die()
	{
		StateMachine.ChangeState(DieState);
	}

	private void OnEnable()
	{
		if(isDead)
		{
			this.CurrentHealth = MaxHealth;
			StateMachine.ChangeState(IdleState);
			isDead = false;
		}
	}

	private void OnDisable()
	{
		isDead = true;
	}


	public void UpdateAttackCoolDown()
	{
		if (attackCooldownTimer < AttackCooldown)
		{
			attackCooldownTimer += Time.deltaTime;
		}
	}

	public bool CheckFinishAttackCoolDown()
	{
		if (attackCooldownTimer >= AttackCooldown)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public virtual void CheckForChangeAttackState()
	{
		if (IsWithinStrikingDistance && CheckFinishAttackCoolDown())
		{
			Vector2 direction = (PlayerPos.CurrentValue - (Vector2)transform.position).normalized;
			if (CheckRaycastAttack())
			{
				StateMachine.ChangeState(AttackState);
			}
		}
	}

	public void SetAnimationIdleAffterHurt()
	{
		animator.SetBool("isDamaged", false);
	}


	// implement interface IEnemyInteractable
	public override void OnEnemyBulletHit(float damage)
	{
		CurrentHealth -= (int)damage;
		animator.SetBool("isDamaged", true);


		if (StateMachine.CurrentState != HurtState && (StateMachine.CurrentState == IdleState || StateMachine.CurrentState == ChaseState))
		{
			StateMachine.ChangeState(HurtState);
		}

		if (CurrentHealth <= 0 && StateMachine.CurrentState != DieState)
		{
			Die();
		}
	}


	// implement interface IExplodedInteractable
	public override void OnExplode(float damage)
	{
		StopAllCoroutines();
		animator.SetBool("isDamaged", true);

		CurrentHealth -= (int)damage;
		StateMachine.ChangeState(KnockbackState);
	}

}
