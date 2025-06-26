using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2AttackLeftState : Lich2State
{
	private float duration = 2.15f;
	private float timeSpawnTrail = 1.5f + 1f / 6f;
	public Lich2AttackLeftState(Lich2 highPriest, Lich2StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("AttackLeft", true);
		boss.StartCoroutine(ExitState());
		boss.StartCoroutine(SpawnTrail());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("AttackLeft", false);
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
		boss.SpawnTrail(boss.trailLeft, 0.1f);
		EffectManager.Instance.PlayEffect(EffectType.Lich2ExplodeLeft, boss.posLeft.position, Quaternion.identity);
	}
}

