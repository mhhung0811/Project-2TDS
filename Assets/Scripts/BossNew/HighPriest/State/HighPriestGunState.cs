using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestGunState : HighPriestState
{
	public HighPriestGunState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Gun", true);
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Gun", false);
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

