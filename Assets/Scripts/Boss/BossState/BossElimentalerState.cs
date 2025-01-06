using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElimentalerState : BossState
{
	private float timeStartSpawnCheese = 0.9f;
	public BossElimentalerState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		Boss.Animator.SetBool("IsElimentaler", true);
		Boss.StartCoroutine(Attack());
		if (!Boss.isFacingRight)
		{
			Boss.Flip();
		}
	}

	public override void Exit()
	{
		base.Exit();
		Boss.Animator.SetBool("IsElimentaler", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void AnimationTriggerEvent(Boss.AnimationTriggerType triggerType)
	{

	}

	public IEnumerator Attack()
	{
		yield return new WaitForSeconds(timeStartSpawnCheese);
		int quantity = 60;
		float angleStart = -90;
		float angleSpace = 360 / (quantity/3);
		for (int i = 0; i < 20; i++)
		{

			Boss.TakeBulletEvent.Raise((FlyweightType.CheeseBullet, Boss.BossPos.CurrentValue, angleStart - i * angleSpace - angleSpace / 3f));
			Boss.TakeBulletEvent.Raise((FlyweightType.CheeseBullet, Boss.BossPos.CurrentValue, angleStart - i * angleSpace));
			Boss.TakeBulletEvent.Raise((FlyweightType.CheeseBullet, Boss.BossPos.CurrentValue, angleStart - i * angleSpace + angleSpace / 3f));

			Boss.TakeBulletEvent.Raise((FlyweightType.CheeseBulletPro, Boss.BossPos.CurrentValue, angleStart - i * angleSpace - angleSpace/ 3f));
			Boss.TakeBulletEvent.Raise((FlyweightType.CheeseBulletPro, Boss.BossPos.CurrentValue, angleStart - i * angleSpace));
			Boss.TakeBulletEvent.Raise((FlyweightType.CheeseBulletPro, Boss.BossPos.CurrentValue, angleStart - i * angleSpace + angleSpace/ 3f));
			
			yield return new WaitForSeconds(1f/40f);
		}
		yield return new WaitForSeconds(2f);
		BossStateMachine.ChangeState(Boss.ThrowKunaiState);
	}
}
