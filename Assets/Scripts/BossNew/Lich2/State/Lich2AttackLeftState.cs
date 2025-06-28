using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2AttackLeftState : Lich2State
{
	private float duration = 2.15f;
	private float timeSpawnTrail = 1.5f + 1f / 6f;
	public Lich2AttackLeftState(Lich2 highPriest, Lich2StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("AttackLeft", true);
		boss.StartCoroutine(ExitState());
		boss.StartCoroutine(Attack());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("AttackLeft", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator ExitState()
	{
		yield return new WaitForSeconds(duration);
		if(boss.idleState.isCombo)
		{
			yield return new WaitForSeconds(1.5f);
			stateMachine.ChangeState(boss.attackRightState);
			yield break;
		}
		boss.stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator Attack()
	{
		yield return new WaitForSeconds(timeSpawnTrail);
		boss.SpawnTrail(boss.trailLeft, 0.1f);
		EffectManager.Instance.PlayEffect(EffectType.Lich2ExplodeLeft, boss.posLeft.position, Quaternion.identity);
		boss.SpawnArcBullets(
			boss.posLeft.position,
			Vector2.down,
			190f,
			30,
			FlyweightType.LichGunBullet
		);

		boss.SpawnArcBullets(
			boss.posLeft.position,
			Vector2.down/6f + Vector2.left,
			30f,
			10,
			FlyweightType.BulletRotate
		);

		boss.StartCoroutine(Attack2());

		yield return new WaitForSeconds(0.1f);
		boss.SpawnArcBullets(
			boss.posLeft.position,
			Vector2.down,
			190f,
			30,
			FlyweightType.LichGunBullet
		);
	}

	private IEnumerator Attack2()
	{
		yield return new WaitForSeconds(2.1f);
		SpawnLinesRandom(
			boss.areaLeft.position,
			Vector2.right,
			8,
			7,
			1f
		);
	}

	private void SpawnLinesRandom(Vector2 posStart, Vector2 dir, int countLine, int amountInLine, float widthStep)
	{
		int randomContinue = Random.Range(1, countLine - 1);
		for (int i = 0; i < countLine; i++)
		{
			if (i == randomContinue) continue;
			boss.StartCoroutine(SpawnLine(
				posStart + Vector2.up * widthStep * i,
				dir,
				amountInLine
			));
		}
	}

	private IEnumerator SpawnLine(Vector2 pos, Vector2 dir, int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			boss.takeBulletFunc.GetFunction()((
				FlyweightType.LichGunBullet,
				pos,
				boss.Vector2ToAngle(dir)
			));

			yield return new WaitForSeconds(0.125f);
		}
	}
}

