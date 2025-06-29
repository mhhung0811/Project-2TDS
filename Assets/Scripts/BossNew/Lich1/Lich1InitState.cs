using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich1InitState : Lich1State
{
	private float initTime = 5.5f;
	public Lich1InitState(Lich1 highPriest, Lich1StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Init", true);
		boss.StartCoroutine(CoolDownInitState());
		boss.StartCoroutine(DelayCallFx());
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
		boss.cameraInit.SetActive(true);
		yield return new WaitForSeconds(initTime);
		boss.stateMachine.ChangeState(boss.idleState);
		boss.FinishInitBossState?.Raise(new Void());
		boss.cameraInit.SetActive(false);
	}

	private IEnumerator DelayCallFx()
	{
		yield return new WaitForSeconds(4.25f);
		EffectManager.Instance.PlayEffect(EffectType.LichFlashInitFx, (Vector2)boss.posGunInit.transform.position , Quaternion.identity);
	}
}

