using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestInitState : HighPriestState
{
	public HighPriestInitState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.StartCoroutine(LogicAnimationGun());
		boss.StartCoroutine(LogicAnimation());
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}


	private IEnumerator LogicAnimation()
	{
		boss.cameraInit.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		boss.animator.SetBool("PullHatDown", true);
		yield return new WaitForSeconds(1.165f);
		boss.animator.SetBool("PullHatDown", false);
		boss.animator.SetBool("Gun", true);
		yield return new WaitForSeconds(1.85f);
		boss.animator.SetBool("Gun", false);
		boss.animator.SetBool("PullHatUp", true);
		yield return new WaitForSeconds(0.5f);
		boss.animator.SetBool("PullHatUp", false);

		stateMachine.ChangeState(boss.idleState);
		boss.FinishInitBossState?.Raise(new Void());
		boss.cameraInit.SetActive(false);
	}

	private IEnumerator LogicAnimationGun()
	{
		yield return new WaitForSeconds(0.8f);
		boss.gunAnimator.SetBool("Init", true);
		yield return new WaitForSeconds(2.2f);
		boss.gunAnimator.SetBool("Init", false);
		boss.gunAnimator.SetBool("Idle", true);
	}
}

