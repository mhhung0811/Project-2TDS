using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich1SummonState : Lich1State
{
	private float summonDuration = 3f;
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
		int amount = 5;
		float totalAngle = 45f;
		float timeBetweenShots = 1.4f / 6f;
		for (int i = 0; i < 4; i++)
		{
			boss.SpawnArcBullets(boss.posGun.transform.position, direction, totalAngle, amount, FlyweightType.LichGunBullet);
			EffectManager.Instance.PlayEffect(EffectType.LichFlashInitFx, boss.posGun.transform.position, Quaternion.identity);
			yield return new WaitForSeconds(timeBetweenShots);
		}
	}
}

