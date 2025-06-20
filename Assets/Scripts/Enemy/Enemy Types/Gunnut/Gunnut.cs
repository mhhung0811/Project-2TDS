using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunnut : Enemy
{
	public PatrolArea patrolArea;
	[HideInInspector]
	public Unit unit;
	// State Machine Variables
	public GunnutMoveState moveState { get; set; }
	public GunnutDieState dieState { get; set; }
	public GunnutAttackState attackState { get; set; }
	public GunnutPatrolState patrolState { get; set; }
	private void Awake()
	{
		unit = GetComponent<Unit>();
		animator = GetComponent<Animator>();
		RB = GetComponent<Rigidbody2D>();

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

	public override void OnEnemyBulletHit(float damage)
	{
		base.OnEnemyBulletHit(damage);
	}
}
