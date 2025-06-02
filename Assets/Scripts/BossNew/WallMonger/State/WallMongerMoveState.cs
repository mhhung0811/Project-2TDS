using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerMoveState : WallMongerState
{
	public WallMongerMoveState(WallMonger boss, WallMongerStateMachine stateMachine) : base(boss, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Move", true);
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Move", false);
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
