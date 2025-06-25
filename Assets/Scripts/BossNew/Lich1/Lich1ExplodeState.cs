using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich1ExplodeState : Lich1State
{
	private float explodeDuration = 1.3f;
	public Lich1ExplodeState(Lich1 highPriest, Lich1StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Explode", true);
		boss.StartCoroutine(CoolDownExplode());
		boss.StartCoroutine(Attack());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Explode", false);

		boss.vfx.SetActive(false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator CoolDownExplode()
	{
		yield return new WaitForSeconds(explodeDuration);
		boss.stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator Attack()
	{
		yield return new WaitForSeconds(0.9f);
		EffectManager.Instance.PlayEffect(EffectType.LichExplodeFx, boss.transform.position, Quaternion.identity);
		boss.SpawnArcBullets(boss.transform.position, boss.AngleToVector2(0), 360, 20, FlyweightType.BulletBouncing);
		yield return new WaitForSeconds(0.2f);
		boss.SpawnArcBullets(boss.transform.position, boss.AngleToVector2(9), 360, 20, FlyweightType.BulletRotate);
	}
}

