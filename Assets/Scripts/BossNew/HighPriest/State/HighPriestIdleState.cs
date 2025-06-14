using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestIdleState : HighPriestState
{
	public HighPriestIdleState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
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
		Debug.Log("Exit Idle State");
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
		yield return new WaitForSeconds(1f);
		stateMachine.ChangeState(boss.shieldState);
	}
}

