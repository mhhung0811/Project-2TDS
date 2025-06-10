using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestAttackState : HighPriestState
{
	public HighPriestAttackState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Attack", true);
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
}

