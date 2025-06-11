using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestFireState : HighPriestState
{
	public HighPriestFireState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.StartCoroutine(LogicAnimation());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Fire", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator LogicAnimation()
	{
		boss.animator.SetBool("Attack", true);
		yield return new WaitForSeconds(0.5f);
		boss.animator.SetBool("Attack", false);
		boss.animator.SetBool("Fire", true);
	}
}

