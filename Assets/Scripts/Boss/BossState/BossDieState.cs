using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDieState : BossState
{
	public BossDieState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
	{
	}
	public override void Enter()
	{
		base.Enter();
		Boss.Animator.SetBool("IsDie", true);
		Boss.StartCoroutine(Die());
	}

	public override void Exit()
	{
		base.Exit();
		Boss.Animator.SetBool("IsDie", false);
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

	public IEnumerator Die()
	{
		yield return new WaitForSeconds(2f);
		Void @void = new Void();
		Boss.BossDied.Raise(@void);
	}
}
