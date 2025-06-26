using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2DieState : Lich2State
{
	private float initTime = 5.5f;
	public Lich2DieState(Lich2 highPriest, Lich2StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Die", true);
		boss.StopAllCoroutines();
		boss.col.enabled = false;
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
}

