using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockbackState : EnemyState
{
	private float timer;
	public EnemyKnockbackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{

	}
	public override void Enter()
	{
		Enemy._animator.SetBool("isHurt", true);
		Enemy._animator.SetBool("isDamaged", true);
		timer = 1f;
		if(Enemy.CurrentHealth <= 0)
		{
			EnemyStateMachine.ChangeState(Enemy.DieState);
		}
		Enemy.StartCoroutine(EndState());
	}

	public override void Exit()
	{
		Enemy._animator.SetBool("isHurt", false);
		Enemy._animator.SetBool("isDamaged", false);
	}

	public override void FrameUpdate()
	{

	}

	public override void PhysicsUpdate()
	{

	}

	public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
	{

	}

	public IEnumerator EndState()
	{
		yield return new WaitForSeconds(timer);
		EnemyStateMachine.ChangeState(Enemy.IdleState);
	}
}
