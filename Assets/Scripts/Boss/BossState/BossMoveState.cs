using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : BossState
{
	public BossMoveState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
	{
	}
	public override void Enter()
	{
		base.Enter();
		Boss.Animator.SetBool("IsMove", true);
	}

	public override void Exit()
	{
		base.Exit();
		Boss.Animator.SetBool("IsMove", false);
		Boss.RB.velocity = Vector2.zero;
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
		Boss.UpdateAnimationByPosPlayer();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
		Boss.Moving();
	}

	public override void AnimationTriggerEvent(Boss.AnimationTriggerType triggerType)
	{

	}
}
