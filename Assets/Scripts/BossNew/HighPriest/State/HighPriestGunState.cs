using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class HighPriestGunState : HighPriestState
{
	bool isAttacking = false;
	Vector2 direction = Vector2.down;
	public HighPriestGunState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.StartCoroutine(LogicAnimation());
		boss.StartCoroutine(LogicAnimationGun());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Gun", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
		direction = (boss.playerPos.CurrentValue - (Vector2)boss.posGun.position).normalized;
		boss.gunAnimator.SetFloat("XInput", direction.x);
		boss.gunAnimator.SetFloat("YInput", direction.y);
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator LogicAnimation()
	{
		boss.animator.SetBool("PullHatDown", true);
		yield return new WaitForSeconds(1.165f);
		boss.animator.SetBool("PullHatDown", false);
		boss.animator.SetBool("Gun", true);
		yield return new WaitForSeconds(10f);
		boss.animator.SetBool("Gun", false);
		boss.animator.SetBool("PullHatUp", true);
		yield return new WaitForSeconds(0.5f);
		boss.animator.SetBool("PullHatUp", false);

		stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator LogicAnimationGun()
	{
		yield return new WaitForSeconds(0.8f);
		boss.gunAnimator.SetBool("Idle", false);
		boss.gunAnimator.SetBool("Up", true);
		yield return new WaitForSeconds(0.5f);
		boss.gunAnimator.SetBool("Up", false);
		boss.gunAnimator.SetBool("Fire", true);
		isAttacking = true;
		boss.StartCoroutine(Attack());
		yield return new WaitForSeconds(10f);
		boss.gunAnimator.SetBool("Fire", false);
		boss.gunAnimator.SetBool("Idle", true);
		isAttacking = false;
	}

	private IEnumerator Attack()
	{
		while (isAttacking)
		{
			boss.takeBulletFunc.GetFunction()((
				FlyweightType.HighPriestGunBullet,
				(Vector2)boss.posGun.position + direction.normalized * 0.75f,
				boss.Vector2ToAngle(direction)
			));
			var pref = EffectManager.Instance.PlayEffect(EffectType.FxFlash1, 1f);
			if(pref)
			{
				pref.transform.position = (Vector2)boss.posGun.position + direction.normalized * 0.75f;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
}

