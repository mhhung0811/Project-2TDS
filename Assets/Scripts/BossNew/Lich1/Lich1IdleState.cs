using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich1IdleState : Lich1State
{
	private float coolDownExplode = 15f;
	private float coolDownSummon = 25f;
	
	private bool canUseExplodeState = false;
	private bool canUseSummonState = false;

	private bool isGunState = false;
	public Lich1IdleState(Lich1 highPriest, Lich1StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Idle", true);
		boss.StartCoroutine(ControllerState());
		boss.StartCoroutine(CoolDownExplode());
		boss.StartCoroutine(CoolDownSummon());
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
		yield return new WaitForSeconds(1f);

		if (canUseExplodeState)
		{
			boss.stateMachine.ChangeState(boss.explodeState);
			boss.StartCoroutine(CoolDownExplode());
			yield break;
		}

		if (canUseSummonState)
		{
			boss.stateMachine.ChangeState(boss.summonState);
			boss.StartCoroutine(CoolDownSummon());
			yield break;
		}

		if(isGunState)
		{
			stateMachine.ChangeState(boss.gunState);
			isGunState = false;
		}
		else
		{
			stateMachine.ChangeState(boss.attackAOEState);
			isGunState = true;
		}
	}

	private IEnumerator CoolDownExplode()
	{
		canUseSummonState = false;
		yield return new WaitForSeconds(coolDownExplode);
		canUseExplodeState = true;
	}

	private IEnumerator CoolDownSummon()
	{
		canUseExplodeState = false;
		yield return new WaitForSeconds(coolDownSummon);
		canUseSummonState = true;
	}
}

