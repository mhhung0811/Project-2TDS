using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich1AttackAOEState : Lich1State
{
	private float attackDuration = 5f;
	public Lich1AttackAOEState(Lich1 highPriest, Lich1StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("AttackAOE", true);
		boss.StartCoroutine(CoolDownAttackAOEState());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("AttackAOE", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator CoolDownAttackAOEState()
	{
		yield return new WaitForSeconds(attackDuration);
		boss.stateMachine.ChangeState(boss.idleState);
	}
}

