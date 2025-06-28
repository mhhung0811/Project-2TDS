using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich1SummonState : Lich1State
{
	private float summonDuration = 3.5f;
	public Lich1SummonState(Lich1 highPriest, Lich1StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Summon", true);
		boss.StartCoroutine(CoolDownIdleState());
		boss.StartCoroutine(SpawnFxBook());
		boss.StartCoroutine(Attack());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Summon", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator CoolDownIdleState()
	{
		yield return new WaitForSeconds(summonDuration);
		boss.stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator SpawnFxBook()
	{
		yield return new WaitForSeconds(0.75f);
		EffectManager.Instance.PlayEffect(EffectType.LichBookSummonFx, boss.posBookFx.transform.position, Quaternion.identity);
	}

	private IEnumerator Attack()
	{
		yield return new WaitForSeconds(7f/6f);

		var direction = boss.playerPos.CurrentValue - (Vector2)boss.posGun.transform.position;
		float timeBetweenShots = 1.4f / 6f;

		// 1st shot
		boss.takeBulletFunc.GetFunction()((
			FlyweightType.Lich2HeadWireBullet,
			boss.posGun.transform.position,
			boss.Vector2ToAngle(direction) + 30f
		));
		EffectManager.Instance.PlayEffect(EffectType.LichFlashInitFx, boss.posGun.transform.position, Quaternion.identity);
		yield return new WaitForSeconds(timeBetweenShots);

		// 2nd shot
		boss.takeBulletFunc.GetFunction()((
			FlyweightType.Lich2HeadWireBullet,
			boss.posGun.transform.position,
			boss.Vector2ToAngle(direction) - 30f
		));
		EffectManager.Instance.PlayEffect(EffectType.LichFlashInitFx, boss.posGun.transform.position, Quaternion.identity);
		yield return new WaitForSeconds(timeBetweenShots);

		// 3rd shot
		boss.takeBulletFunc.GetFunction()((
			FlyweightType.Lich2HeadWireBullet,
			boss.posGun.transform.position,
			boss.Vector2ToAngle(direction) + 10f
		));
		EffectManager.Instance.PlayEffect(EffectType.LichFlashInitFx, boss.posGun.transform.position, Quaternion.identity);
		yield return new WaitForSeconds(timeBetweenShots);

		// 4th shot
		boss.takeBulletFunc.GetFunction()((
			FlyweightType.Lich2HeadWireBullet,
			boss.posGun.transform.position,
			boss.Vector2ToAngle(direction) - 10f
		));
		EffectManager.Instance.PlayEffect(EffectType.LichFlashInitFx, boss.posGun.transform.position, Quaternion.identity);
		yield return new WaitForSeconds(timeBetweenShots + 0.5f);

		// final shot
		boss.takeBulletFunc.GetFunction()((
			FlyweightType.Lich2HeadWireBullet,
			boss.posGun.transform.position,
			boss.Vector2ToAngle(boss.playerPos.CurrentValue - (Vector2)boss.posGun.transform.position)
		));
		EffectManager.Instance.PlayEffect(EffectType.LichFlashInitFx, boss.posGun.transform.position, Quaternion.identity);
	}
}

