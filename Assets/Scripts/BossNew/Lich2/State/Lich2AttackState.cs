using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2AttackState : Lich2State
{
	private float duration = 5f;
	public Lich2AttackState(Lich2 highPriest, Lich2StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Attack", true);
		boss.StartCoroutine(ExitState());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Attack", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator ExitState()
	{
		yield return new WaitForSeconds(duration);
		boss.stateMachine.ChangeState(boss.idleState);
	}
}

