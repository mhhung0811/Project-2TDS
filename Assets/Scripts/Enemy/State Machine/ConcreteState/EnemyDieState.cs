using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : EnemyState
{
	private float dieTime;
	public EnemyDieState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{
	}

	public override void Enter()
	{
		dieTime = 0;
		Enemy._animator.SetBool("isDie", true);
		Debug.Log("Enemy Die");
	}

	public override void Exit()
	{
		dieTime = 0;
		Enemy._animator.SetBool("isDie", false);
		Debug.Log("Enemy Die Exit");
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
		dieTime += Time.deltaTime;
		if (dieTime >= Enemy.DieTime)
		{
			Enemy.gameObject.SetActive(false);
		}
	}
}
