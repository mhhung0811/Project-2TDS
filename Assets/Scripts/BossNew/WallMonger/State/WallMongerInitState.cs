using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerInitState : WallMongerState
{
	public WallMongerInitState(WallMonger boss, WallMongerStateMachine stateMachine) : base(boss, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Init", true);
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Init", false);
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
