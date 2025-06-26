using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2AttackCenterState : Lich2State
{
	private float duration = 2f;
	private float timeSpawnTrail = 1.5f;
	public Lich2AttackCenterState(Lich2 highPriest, Lich2StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("AttackCenter", true);
		boss.StartCoroutine(ExitState());
		boss.StartCoroutine(SpawnTrail());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("AttackCenter", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator ExitState()
	{
		yield return new WaitForSeconds(duration);
		boss.stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator SpawnTrail()
	{
		yield return new WaitForSeconds(timeSpawnTrail);
		boss.SpawnTrail(boss.trailCenter, 0.075f);
	}
}

