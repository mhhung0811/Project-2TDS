using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowKunaiState : BossState
{
	public BossThrowKunaiState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		Boss.Animator.SetBool("IsThrowKunai", true);
		Boss.StartCoroutine(Attack());
	}

	public override void Exit()
	{
		base.Exit();
		Boss.Animator.SetBool("IsThrowKunai", false);
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
		// Lan 1 => 4 kunai
		Boss.StartCoroutine(ThrowKunai(5));
		yield return new WaitForSeconds(1.5f);

		// Lan 2 => 7 kunai
		Boss.Animator.SetBool("IsIdle", false);
		Boss.Animator.SetBool("IsThrowKunai", true);
		Boss.StartCoroutine(ThrowKunai(8));
		yield return new WaitForSeconds(1.5f);

		// Lam 3 => 10 kunai
		Boss.Animator.SetBool("IsIdle", false);
		Boss.Animator.SetBool("IsThrowKunai", true);
		Boss.StartCoroutine(ThrowKunai(12));

		yield return new WaitForSeconds(2f);
		BossStateMachine.ChangeState(Boss.IdleState);
	}

	public IEnumerator ThrowKunai(int quantity)
	{
		yield return new WaitForSeconds(0.25f);
		float spaceAngle = 8;
		float totalAngle = quantity * spaceAngle;
		Vector2 directionToPlayer = (Boss.PlayerPos.CurrentValue - Boss.BossPos.CurrentValue).normalized;
		float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

		float startAngle = baseAngle - totalAngle / 2;

		for (int i = 0; i < quantity; i++)
		{
			float currentAngle = startAngle + i * spaceAngle;
			Vector2 directionLine = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

			GameObject lineLaze = EffectManager.Instance.PlayEffect(EffectType.SpawnLineLaze, Boss.BossPos.CurrentValue, Quaternion.identity);
			EffectLineLaze effectLineLaze = lineLaze.GetComponent<EffectLineLaze>();
			effectLineLaze.Initialize(directionLine, 0.5f);
		}

		yield return new WaitForSeconds(0.3f);

		for (int i = 0; i < quantity; i++)
		{
			float currentAngle = startAngle + i * spaceAngle;
			Boss.TakeBulletEvent.Raise((FlyweightType.KunaiBullet, Boss.BossPos.CurrentValue, currentAngle));
		}

		yield return new WaitForSeconds(0.3f);

		for (int i = 0; i < quantity; i++)
		{
			float currentAngle = startAngle + i * spaceAngle;
			Boss.TakeBulletEvent.Raise((FlyweightType.KunaiBullet, Boss.BossPos.CurrentValue, currentAngle));
		}

		yield return new WaitForSeconds(0.3f);
		Boss.Animator.SetBool("IsIdle", true);
		Boss.Animator.SetBool("IsThrowKunai", false);
	}

}
