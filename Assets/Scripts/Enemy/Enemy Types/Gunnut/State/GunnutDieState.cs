using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnutDieState : EnemyState
{
	private Gunnut gunnut => (Gunnut)base.Enemy;

	public GunnutDieState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void FrameUpdate()
	{

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
