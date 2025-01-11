using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveToCenterState : BossState
{
	public BossMoveToCenterState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
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

		if(Boss.isStayPosCenter)
		{
			BossStateMachine.ChangeState(Boss.SummonCheeseState);
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
		Boss.MoveToPosCenter();
	}

	public override void AnimationTriggerEvent(Boss.AnimationTriggerType triggerType)
	{

	}
}
