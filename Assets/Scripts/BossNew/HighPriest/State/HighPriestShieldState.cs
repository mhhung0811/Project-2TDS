using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestShieldState : HighPriestState
{
	public HighPriestShieldState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Shield", true);
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Shield", false);
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

