using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : BossState
{
	private float timeToChangeState = 1f;
	public BossMoveState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
	{
	}
	public override void Enter()
	{
		base.Enter();
		Boss.Animator.SetBool("IsMove", true);
		Boss.StartCoroutine(CanExitState());
	}

	public override void Exit()
	{
		base.Exit();
		Boss.Animator.SetBool("IsMove", false);
		Boss.RB.velocity = Vector2.zero;
		Boss.isMoveStatePrevious = true;
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

	public IEnumerator CanExitState()
	{
		yield return new WaitForSeconds(timeToChangeState);
		BossStateMachine.ChangeState(Boss.IdleState);
	}
}
