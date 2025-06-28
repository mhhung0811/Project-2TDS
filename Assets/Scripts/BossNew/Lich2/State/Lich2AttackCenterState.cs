using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2AttackCenterState : Lich2State
{
	private float duration = 2f;
	private float timeSpawnTrail = 1.5f;
	public Lich2AttackCenterState(Lich2 highPriest, Lich2StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("AttackCenter", true);
		boss.StartCoroutine(ExitState());
		boss.StartCoroutine(Attack());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("AttackCenter", false);
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
			// End combo
			boss.idleState.isCombo = false;
		}
		boss.stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator Attack()
	{
		yield return new WaitForSeconds(timeSpawnTrail);
		boss.SpawnTrail(boss.trailCenter, 0.1f);
		EffectManager.Instance.PlayEffect(EffectType.Lich2ExplodeCenter, boss.posCenter.position, Quaternion.identity);

		if(boss.idleState.isCombo)
		{
			boss.StartCoroutine(Skill2());
			yield break;
		}

		boss.StartCoroutine(Skill1());
	}

	private IEnumerator Skill1()
	{
		// Spawn Left
		boss.StartCoroutine(SpawnGrids(
			pos: boss.areaLeft.position,
			dir: Vector2.right,
			countLine: 5,
			amountInLine: 5,
			widthStep: 2f,
			countGrid: 5
		));

		yield return new WaitForSeconds(1f);
		// Spawn Right
		boss.StartCoroutine(SpawnGrids(
			pos: (Vector2)boss.areaRight.position + Vector2.up * 0.75f,
			dir: Vector2.left,
			countLine: 4,
			amountInLine: 5,
			widthStep: 2f,
			countGrid: 5
		));
	}

	public IEnumerator SpawnGrids(Vector2 pos, Vector2 dir, int countLine, int amountInLine, float widthStep, int countGrid)
	{
		for(int i =0; i < countGrid; i++)
		{
			SpawnLines(
				pos,
				dir,
				countLine,
				amountInLine,
				widthStep
			);
			yield return new WaitForSeconds(2f);
		}
	}

	private void SpawnLines(Vector2 posStart, Vector2 dir, int countLine, int amountInLine, float widthStep) {
		for(int i = 0; i< countLine;i++)
		{
			boss.StartCoroutine(SpawnLine(
				posStart + Vector2.up * widthStep * i,
				dir,
				amountInLine
			));
		}
	}

	private IEnumerator SpawnLine(Vector2 pos, Vector2 dir, int amount,FlyweightType type = FlyweightType.LichGunBullet)
	{
		for (int i = 0; i < amount; i++) {
			boss.takeBulletFunc.GetFunction()((
				type,
				pos,
				boss.Vector2ToAngle(dir)
			));

			yield return new WaitForSeconds(0.15f);
		}
	}

	// ---------- Skill2
	private IEnumerator Skill2()
	{
		// Center
		boss.SpawnArcBullets(
			boss.posLeft.position,
			Vector2.down,
			190f,
			30,
			FlyweightType.LichGunBullet
		);
		// Left
		boss.SpawnArcBullets(
			boss.posLeft.position,
			Vector2.down / 6f + Vector2.left,
			30f,
			10,
			FlyweightType.BulletRotate
		);
		// Right
		boss.SpawnArcBullets(
			boss.posRight.position,
			Vector2.down / 6f + Vector2.right,
			30f,
			10,
			FlyweightType.BulletRotate
		);

		boss.StartCoroutine(LinesRandom());

		yield return new WaitForSeconds(0.1f);
		boss.SpawnArcBullets(
			boss.posLeft.position,
			Vector2.down,
			190f,
			30,
			FlyweightType.LichGunBullet
		);
	}

	private IEnumerator LinesRandom()
	{
		yield return new WaitForSeconds(2.1f);
		// Left
		SpawnLinesRandomContinue(
			boss.areaLeft.position,
			Vector2.right,
			8,
			7,
			1f
		);
		// Right
		SpawnLinesRandomContinue(
			boss.areaRight.position,
			Vector2.left,
			8,
			7,
			1f
		);
	}

	private void SpawnLinesRandomContinue(Vector2 posStart, Vector2 dir, int countLine, int amountInLine, float widthStep)
	{
		int randomContinue = Random.Range(1, countLine - 1);
		for (int i = 0; i < countLine; i++)
		{
			if (i == randomContinue) continue;
			boss.StartCoroutine(SpawnLine(
				posStart + Vector2.up * widthStep * i,
				dir,
				amountInLine,
				FlyweightType.BulletBouncing
			));
		}
	}
}

