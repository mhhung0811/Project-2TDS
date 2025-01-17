using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInitState : BossState
{
	public BossInitState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
	{
	}
	public override void Enter()
	{
		base.Enter();
		Boss.Animator.SetBool("IsInit", true);
		Boss.StartCoroutine(EndInit());
	}

	public override void Exit()
	{
		base.Exit();
		Boss.Animator.SetBool("IsInit", false);
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

	public IEnumerator EndInit()
	{
		yield return new WaitForSeconds(3.5f);
		BossStateMachine.ChangeState(Boss.MoveToCenterState);
	}
}
