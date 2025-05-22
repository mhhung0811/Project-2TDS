using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShee : Enemy
{
	public SkillBombShee SkillBombShee { get; set; }

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

		StateMachine.Initialize(IdleState);
	}

	private void Update()
	{
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
	#endregion
}
