using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnutPatrolState : EnemyState
{
	private Vector2 currentPatrolPoint;
	private int currentPatrolIndex;
	private Gunnut gunnut => (Gunnut)base.Enemy;

	public GunnutPatrolState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		gunnut.animator.SetBool("IsMove", true);
		currentPatrolIndex = 0;
		currentPatrolPoint = gunnut.patrolArea.patrolPoints[currentPatrolIndex].position;

		gunnut.unit.speed = gunnut.MoveSpeed * 0.8f;
		gunnut.unit.StartFindPathPoint(currentPatrolPoint);
	}

	public override void Exit()
	{
		base.Exit();
		gunnut.animator.SetBool("IsMove", false);
		gunnut.unit.speed = 0f;
		gunnut.unit.StopFindPath();
	}

	public override void FrameUpdate()
	{
		gunnut.animator.SetFloat("XInput", gunnut.RB.velocity.x);
		gunnut.animator.SetFloat("YInput", gunnut.RB.velocity.y);

		if (CheckIfArrivedAtPatrolPoint())
		{
			// Change to the next patrol point
			ChooseNextPatrolPoint();
			// StartFindPath to the next patrol point
			gunnut.unit.StartFindPathPoint(currentPatrolPoint);
		}

		if (Enemy.IsWithinStrikingDistance)
		{
			EnemyStateMachine.ChangeState(gunnut.moveState);
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}


	private bool CheckIfArrivedAtPatrolPoint()
	{
		if (Vector2.Distance(gunnut.unit.transform.position, currentPatrolPoint) < 0.6f)
		{
			return true;
		}
		return false;
	}

	private void ChooseNextPatrolPoint()
	{
		currentPatrolIndex++;
		if (currentPatrolIndex >= gunnut.patrolArea.patrolPoints.Count)
		{
			currentPatrolIndex = 0;
		}
		currentPatrolPoint = gunnut.patrolArea.patrolPoints[currentPatrolIndex].position;
	}
}
