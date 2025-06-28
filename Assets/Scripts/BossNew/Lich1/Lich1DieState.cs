using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich1DieState : Lich1State
{
	public Lich1DieState(Lich1 highPriest, Lich1StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Die", true);
		boss.StopAllCoroutines();
		boss.col.enabled = false;
		boss.StartCoroutine(DestroyDelay());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Die", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator DestroyDelay()
	{
		yield return new WaitForSeconds(1.25f);
		boss.gameObject.SetActive(false);
	}
}

