using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestDissolveState : HighPriestState
{
	public HighPriestDissolveState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Dissolve", true);
	}

	public override void Exit()
	{
		Debug.Log("Exit Idle State");
		base.Exit();
		boss.animator.SetBool("Dissolve", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}

