using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerDieState : WallMongerState
{
	public WallMongerDieState(WallMonger boss, WallMongerStateMachine stateMachine) : base(boss, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Die", true);
		boss.vfx.PlayVFXDie();

		// Delay Do die
		boss.StartCoroutine(DoDelayDieState());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Die", false);
		boss.vfx.ExitVFXDie();

	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public IEnumerator DoDelayDieState()
	{
		yield return new WaitForSeconds(1f);
		boss.vfx.ExitVFXDie();

		boss.col.enabled = false;

		// Set collider
		boss.colliderAlive.SetActive(false);
		boss.colliderDie.SetActive(true);

		// Set OrderinLayer
		boss.spriteRenderer.sortingOrder = 10;
	}
}
