using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerJumpState : WallMongerState
{
	public WallMongerJumpState(WallMonger boss, WallMongerStateMachine stateMachine) : base(boss, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Jump", true);
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Jump", false);
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
