using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRollState : BossState
{
	public BossRollState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
	{
	}
	public override void Enter()
	{
		base.Enter();
		Boss.StartCoroutine(Roll());
	}

	public override void Exit()
	{
		base.Exit();
		Boss.Animator.SetBool("IsRoll", false);
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

	public IEnumerator Roll()
	{
		int xInput = Random.Range(-1, 2);
		int yInput = Random.Range(-1, 2);
		Boss.Animator.SetBool("IsRoll", true);
		Boss.Animator.SetFloat("XInput",xInput);
		Boss.Animator.SetFloat("YInput", yInput);
		Boss.RB.velocity = new Vector2(xInput, yInput).normalized * 8f;

		yield return new WaitForSeconds(0.4f);
		Boss.RB.velocity = Vector2.zero;
		yield return new WaitForSeconds(0.5f);
		BossStateMachine.ChangeState(Boss.IdleState);
	}

	public void FlipRight()
	{
		if (!Boss.isFacingRight)
		{
			Boss.Flip();
		}
	}
}
