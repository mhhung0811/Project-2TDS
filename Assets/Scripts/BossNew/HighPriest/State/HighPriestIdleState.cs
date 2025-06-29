using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestIdleState : HighPriestState
{
	private bool canUseShieldState = false;
	private bool canUseGunState = false;
	private bool canUseDissolveState = false;

	private float cooldownShield = 20f;
	private float cooldownGun = 40f;
	private float cooldownDissolve = 40f;
	public HighPriestIdleState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{
		boss.StartCoroutine(CoolDownShield());
		boss.StartCoroutine(CoolDownGun());
		boss.StartCoroutine(CoolDownDissolve());
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
		boss.Move();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator ControllerState()
	{
		yield return new WaitForSeconds(1f);
		
		if(canUseDissolveState)
		{
			boss.stateMachine.ChangeState(boss.dissolveState);
			boss.StartCoroutine(CoolDownDissolve());
			yield break;
		}

		if (canUseShieldState)
		{
			boss.stateMachine.ChangeState(boss.shieldState);
			boss.StartCoroutine(CoolDownShield());
			yield break;
		}

		if (canUseGunState)
		{
			boss.stateMachine.ChangeState(boss.gunState);
			boss.StartCoroutine(CoolDownGun());
			yield break;
		}

		int random = Random.Range(0, 3);
		switch(random)
		{
			case 0:
				boss.stateMachine.ChangeState(boss.attackState);
				break;
			case 1:
				boss.stateMachine.ChangeState(boss.teleState);
				break;
			case 2:
				boss.stateMachine.ChangeState(boss.fireState);
				break;
		}
	}

	private IEnumerator CoolDownShield()
	{
		canUseShieldState = false;
		yield return new WaitForSeconds(cooldownShield);
		canUseShieldState = true;
	}

	private IEnumerator CoolDownGun()
	{
		canUseGunState = false;
		yield return new WaitForSeconds(cooldownGun);
		canUseGunState = true;
	}

	private IEnumerator CoolDownDissolve()
	{
		canUseDissolveState = false;
		yield return new WaitForSeconds(cooldownDissolve);
		canUseDissolveState = true;
	}
}

