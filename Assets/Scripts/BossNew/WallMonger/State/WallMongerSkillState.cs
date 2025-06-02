using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerSkillState : WallMongerState
{
	public WallMongerSkillState(WallMonger boss, WallMongerStateMachine stateMachine) : base(boss, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Skill", true);
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Skill", false);
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
