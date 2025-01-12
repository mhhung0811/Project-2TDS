using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheeseSlamState : BossState
{
	public BossCheeseSlamState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		Boss.Animator.SetBool("IsCheeseSlam", true);
		Boss.StartCoroutine(EndState());
	}

	public override void Exit()
	{
		base.Exit();
		Boss.Animator.SetBool("IsCheeseSlam", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void AnimationTriggerEvent(Boss.AnimationTriggerType triggerType)
	{

	}

	public IEnumerator EndState()
	{
		yield return new WaitForSeconds(2f);
		BossStateMachine.ChangeState(Boss.IdleState);
	}
}
