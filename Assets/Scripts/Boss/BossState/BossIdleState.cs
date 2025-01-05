using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
	public BossIdleState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		Boss.Animator.SetBool("IsIdle", true);
	}

	public override void Exit()
	{
		base.Exit();
		Boss.Animator.SetBool("IsIdle", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
		Boss.UpdateAnimationByPosPlayer();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void AnimationTriggerEvent(Boss.AnimationTriggerType triggerType)
	{
		
	}
}

