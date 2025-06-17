using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerIdleState : WallMongerState
{
	private float timeExitState = 1f;
	public WallMongerIdleState(WallMonger boss, WallMongerStateMachine stateMachine) : base(boss, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Idle", true);

		boss.StartCoroutine(ControllerState());
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

	private IEnumerator ControllerState()
	{
		yield return new WaitForSeconds(timeExitState);

		if(boss.canUseSkill)
		{
			boss.UseSkill();
		}
		else
		{
			stateMachine.ChangeState(boss.moveState);
		}
	}
}
