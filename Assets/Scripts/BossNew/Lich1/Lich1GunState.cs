using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich1GunState : Lich1State
{
	private float gunTime = 5.5f/6f;
	public Lich1GunState(Lich1 highPriest, Lich1StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Gun", true);
		boss.StartCoroutine(Attack());
		boss.StartCoroutine(CoolDownGunState());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Gun", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
	private IEnumerator CoolDownGunState()
	{
		yield return new WaitForSeconds(gunTime);
		boss.stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator Attack()
	{
		yield return new WaitForSeconds(1f/6f);

		var direction = boss.playerPos.CurrentValue - (Vector2)boss.posGun.transform.position;
		int amount = 5;
		float totalAngle = 45f;
		float timeBetweenShots = 1f/6f;
		for (int i = 0; i< 4; i++)
		{
			boss.SpawnArcBullets(boss.posGun.transform.position, direction, totalAngle, amount);
			EffectManager.Instance.PlayEffect(EffectType.LichFlashInitFx, boss.posGun.transform.position, Quaternion.identity);
			yield return new WaitForSeconds(timeBetweenShots);
		}
	}
}

