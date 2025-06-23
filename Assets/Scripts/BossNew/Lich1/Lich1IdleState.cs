using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich1IdleState : Lich1State
{
	public Lich1IdleState(Lich1 highPriest, Lich1StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Idle", true);
		boss.StartCoroutine(CoolDownIdleState());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Idle", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator CoolDownIdleState()
	{
		yield return new WaitForSeconds(1f);
		boss.stateMachine.ChangeState(boss.explodeState);
	}
}

