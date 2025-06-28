using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2InitState : Lich2State
{
	private float initTime = 4.5f;
	private float timeSpawnTrail = 4f + 1f/6f;

	public Lich2InitState(Lich2 highPriest, Lich2StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Init", true);
		boss.StartCoroutine(CoolDownInitState());
		boss.StartCoroutine(SpawnTrail());
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

	private IEnumerator CoolDownInitState()
	{
		yield return new WaitForSeconds(initTime);
		boss.stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator SpawnTrail()
	{
		yield return new WaitForSeconds(timeSpawnTrail);
		boss.SpawnTrail(boss.trailCenter, 0.1f);
		EffectManager.Instance.PlayEffect(EffectType.Lich2ExplodeCenter, boss.posCenter.position, Quaternion.identity);
	}
}

