using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyState
{
	private float hurtTime;
	private EnemyUseGun enemyUseGun => (EnemyUseGun)base.Enemy;
	public EnemyHurtState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{
	}

	public override void Enter()
	{
		hurtTime = 0;
		Enemy.animator.SetBool("isHurt", true);
		// Debug.Log("Enemy Hurt");
	}

	public override void Exit()
	{
		hurtTime = 0;
		Enemy.animator.SetBool("isHurt", false);
		// Debug.Log("Enemy Hurt Exit");
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
		hurtTime += Time.deltaTime;
		if (hurtTime >= 0.2)
		{
			EnemyStateMachine.ChangeState(enemyUseGun.IdleState);
		}
	}
}
