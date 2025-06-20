using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnutMoveState : EnemyState
{
	private Gunnut gunnut => (Gunnut)base.Enemy;

	public GunnutMoveState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		gunnut.animator.SetBool("IsMove", true);
		gunnut.unit.speed = gunnut.MoveSpeed;
		gunnut.unit.StartFindPath();
	}

	public override void Exit()
	{
		base.Exit();
		gunnut.animator.SetBool("IsMove", false);
		gunnut.unit.speed = 0;
		gunnut.unit.StopFindPath();
	}

	public override void FrameUpdate()
	{
		gunnut.animator.SetFloat("XInput", gunnut.RB.velocity.x);
		gunnut.animator.SetFloat("YInput", gunnut.RB.velocity.y);

		if (!gunnut.patrolArea.CheckEnemyInPatrolArea(Enemy.transform.position) && !gunnut.IsWithinStrikingDistance)
		{
			EnemyStateMachine.ChangeState(gunnut.patrolState);
			return;
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
