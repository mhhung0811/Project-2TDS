using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerInitState : WallMongerState
{
	private float timeExitState = 2f;
	public WallMongerInitState(WallMonger boss, WallMongerStateMachine stateMachine) : base(boss, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Init", true);

		boss.StartCoroutine(DelayExitState());
		boss.cameraBossInit.SetActive(true);
		boss.cameraBoss.gameObject.SetActive(false);
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Init", false);
		boss.cameraBossInit.SetActive(false);
		boss.cameraBoss.gameObject.SetActive(true);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator DelayExitState()
	{
		yield return new WaitForSeconds(timeExitState);
		boss.animator.SetBool("Init", false);
		boss.animator.SetBool("Jump", true);
		yield return new WaitForSeconds(1.2f);
		boss.animator.SetBool("Jump", false);
		boss.animator.SetBool("Idle", true);
		yield return new WaitForSeconds(1f);
		stateMachine.ChangeState(boss.idleState);
	}
}
