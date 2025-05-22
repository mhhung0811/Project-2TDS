using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSheeAttackState : EnemyState
{
	private BombShee bombShee => (BombShee)base.Enemy;

	public BombSheeAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
	{

	}

	public override void Enter()
	{
		bombShee.animator.SetBool("IsAttack", true);
	}

	public override void Exit()
	{
		bombShee.animator.SetBool("IsAttack", false);
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
}
