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
		Boss.isRolling = true;
		Boss.IsEnemyInteractable = false;
		Boss.StartCoroutine(Roll());
		Boss.StartCoroutine(RollCoolDown());
	}

	public override void Exit()
	{
		base.Exit();
		Boss.isRolling = false;
		Boss.canRoll = false;
		Boss.IsEnemyInteractable = true;
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
		Vector2 direction = Boss.PlayerPos.CurrentValue - Boss.BossPos.CurrentValue;
		Boss.Animator.SetBool("IsRoll", true);
		Boss.Animator.SetFloat("XInput",direction.x);
		Boss.Animator.SetFloat("YInput", direction.y);
		Boss.RB.velocity = direction.normalized * Boss.moveSpeed*1.25f;

		yield return new WaitForSeconds(0.4f/0.6f);
		Boss.RB.velocity = Vector2.zero;
		yield return new WaitForSeconds(0.2f);
		BossStateMachine.ChangeState(Boss.IdleState);
	}

	public IEnumerator RollCoolDown()
	{
		yield return new WaitForSeconds(Boss.cooldownRoll);
		Boss.canRoll = true;
	}
}
