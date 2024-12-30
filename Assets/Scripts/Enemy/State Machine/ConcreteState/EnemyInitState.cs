using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitState : EnemyState
{
	private float initTimer;
	public EnemyInitState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{
	}

	public override void Enter()
	{
		initTimer = 0;
		Enemy._animator.SetBool("isInit", true);
		Debug.Log("Enemy Init State");
	}

	public override void Exit()
	{
		initTimer = 0;
		Enemy._animator.SetBool("isInit", false);
	}

	public override void FrameUpdate()
	{
		CheckForChangeState();
	}

	public override void PhysicsUpdate()
	{
		
	}

	public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
	{
		
	}

	private void CheckForChangeState()
	{
		initTimer += Time.deltaTime;
		if (initTimer >= Enemy.InitTime)
		{
			EnemyStateMachine.ChangeState(Enemy.IdleState);
		}
	}
}
