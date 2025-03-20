using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
	private Unit _unit;
	private Vector2 _currentPatrolPoint;
	private int _currentPatrolIndex;

	public EnemyPatrolState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{
		_unit = enemy.GetComponent<Unit>();
	}

	public override void Enter()
	{
		base.Enter();

		Enemy._animator.SetBool("isChasing", true);
		_currentPatrolIndex = 0;
		_currentPatrolPoint = Enemy.patrolArea.patrolPoints[_currentPatrolIndex].position;

		_unit.speed = Enemy.MoveSpeed*0.8f;
		_unit.StartFindPathPoint(_currentPatrolPoint);
		Debug.Log("Patrol State");
	}

	public override void Exit()
	{
		base.Exit();
		Enemy._animator.SetBool("isChasing", false);
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
		if (_currentPatrolIndex >= Enemy.patrolArea.patrolPoints.Count)
		{
			_currentPatrolIndex = 0;
		}
		_currentPatrolPoint = Enemy.patrolArea.patrolPoints[_currentPatrolIndex].position;
	}
}
