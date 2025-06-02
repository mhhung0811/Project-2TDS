using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
	private Unit _unit;
	private Vector2 _currentPatrolPoint;
	private int _currentPatrolIndex;
	private EnemyUseGun enemyUseGun => (EnemyUseGun)base.Enemy;

	public EnemyPatrolState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{
		_unit = enemy.GetComponent<Unit>();
	}

	public override void Enter()
	{
		base.Enter();

		enemyUseGun.animator.SetBool("isChasing", true);
		_currentPatrolIndex = 0;
		_currentPatrolPoint = enemyUseGun.patrolArea.patrolPoints[_currentPatrolIndex].position;

		_unit.speed = enemyUseGun.MoveSpeed*0.8f;
		_unit.StartFindPathPoint(_currentPatrolPoint);
		Debug.Log("Patrol State");
	}

	public override void Exit()
	{
		base.Exit();
		enemyUseGun.animator.SetBool("isChasing", false);
		_unit.speed = 0;
		_unit.StopFindPath();
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
		if (CheckIfArrivedAtPatrolPoint())
		{
			// Change to the next patrol point
			ChooseNextPatrolPoint();
			// StartFindPath to the next patrol point
			_unit.StartFindPathPoint(_currentPatrolPoint);
		}

		if (Enemy.IsWithinStrikingDistance && Enemy.CheckRaycastAttack())
		{
			EnemyStateMachine.ChangeState(enemyUseGun.AttackState);
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
	{
		base.AnimationTriggerEvent(triggerType);
	}

	private bool CheckIfArrivedAtPatrolPoint()
	{
		if (Vector2.Distance(_unit.transform.position, _currentPatrolPoint) < 0.6f)
		{
			return true;
		}
		return false;
	}

	private void ChooseNextPatrolPoint()
	{
		_currentPatrolIndex++;
		if (_currentPatrolIndex >= enemyUseGun.patrolArea.patrolPoints.Count)
		{
			_currentPatrolIndex = 0;
		}
		_currentPatrolPoint = enemyUseGun.patrolArea.patrolPoints[_currentPatrolIndex].position;
	}
}
