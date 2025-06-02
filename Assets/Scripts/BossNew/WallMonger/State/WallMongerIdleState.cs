using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerIdleState : WallMongerState
{
	public WallMongerIdleState(WallMonger boss, WallMongerStateMachine stateMachine) : base(boss, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Idle", true);
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
}
