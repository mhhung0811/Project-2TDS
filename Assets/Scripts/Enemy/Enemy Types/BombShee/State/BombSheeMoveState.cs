using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSheeMoveState : EnemyState
{
	private Unit _unit;
	private BombShee bombShee => (BombShee)base.Enemy;

	public BombSheeMoveState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
	{
		_unit = enemy.GetComponent<Unit>();
	}

	public override void Enter()
	{
		Debug.Log("BombShee: Enter MoveState");
		_unit.speed = bombShee.MoveSpeed;
		_unit.target.CurrentValue = (Vector2)bombShee.enemyTarget.transform.position;
		_unit.StartFindPath();
	}

	public override void Exit()
	{
		Debug.Log("BombShee: Exit MoveState");
		_unit.speed = 0;
		_unit.StopFindPath();
		_unit.StopAllCoroutines();
		bombShee.RB.velocity = Vector2.zero;
	}

	public override void FrameUpdate()
	{
		_unit.target.CurrentValue = (Vector2)bombShee.enemyTarget.transform.position;
		if(bombShee.OnProtectRange())
		{
			bombShee.StateMachine.ChangeState(bombShee.IdleState);
		}
	}

	public override void PhysicsUpdate()
	{

	}

	public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
	{

	}
}
