using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lich2AttackState : Lich2State
{
	private float delayAttack = 2.5f;
	public bool preSkill1 = false;
	public Lich2AttackState(Lich2 highPriest, Lich2StateMachine stateMachine) : base(highPriest, stateMachine)
	{
		preSkill1 = false;
	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Attack", true);
		if (!preSkill1)
		{
			boss.StartCoroutine(Skill1());
			preSkill1 = true;
		}
		else
		{
			boss.StartCoroutine(Skill2());
			preSkill1 = false; 
		}
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Attack", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
	
	private IEnumerator Skill1()
	{
		yield return new WaitForSeconds(delayAttack);
		for (int i = 0; i< 5;i++)
		{
			SpawnArcBendingBullets(
				boss.areaCenter.transform.position,
				Vector2.down,
				150f,
				15,
				i * 0.08f
			);
		}
		yield return new WaitForSeconds(0.5f);
		boss.StartCoroutine(SpawnRandom());

		yield return new WaitForSeconds(5f);
		stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator Skill2()
	{
		yield return new WaitForSeconds(delayAttack);
		boss.StartCoroutine(SpawnRandomHeadWire());
		yield return new WaitForSeconds(3f);
		boss.animator.SetBool("Idle", true);
		yield return new WaitForSeconds(0.5f);
		boss.animator.SetBool("Idle", false);
		yield return new WaitForSeconds(delayAttack - 0.5f);
		EffectManager.Instance.PlayEffect(EffectType.Lich2ExplodeCenter, boss.areaCenter.position, Quaternion.identity);
		boss.SpawnArcBullets(
			boss.areaCenter.transform.position,
			Vector2.down,
			150f,
			12,
			FlyweightType.Lich2HeadWireBullet
		);
		stateMachine.ChangeState(boss.idleState);
	}

	public void SpawnArcBendingBullets(Vector2 pos, Vector2 direction, float totalAngle, int bulletCount, float delayMove)
	{
		float startAngle = -totalAngle / 2f;
		float stepAngle = totalAngle / (bulletCount - 1);

		for (int i = 0; i < bulletCount; i++)
		{
			float angle = startAngle + i * stepAngle;

			var obj = boss.takeBulletFunc.GetFunction()((
				FlyweightType.Lich2BendingBullet,
				pos,
				boss.Vector2ToAngle(direction) + angle
			));
			obj.GetComponent<Lich2BendingBullet>().delayMove = delayMove;
		}
	}

	private IEnumerator SpawnRandom()
	{
		for(int i = 0; i < 30; i++)
		{
			Vector2 pos = (Vector2)boss.areaCenter.transform.position + Random.insideUnitCircle * 0.6f;
			SpawnBendingLine(
				pos,
				boss.AngleToVector2(Random.Range(200f, 340f)),
				5
			);
			yield return new WaitForSeconds(0.15f);
		}
	}

	private IEnumerator SpawnRandomHeadWire()
	{
		for (int i = 0; i < 8; i++)
		{
			Vector2 pos = (Vector2)boss.areaCenter.transform.position + Random.insideUnitCircle * 0.4f;
			boss.takeBulletFunc.GetFunction()((
				FlyweightType.Lich2HeadWireBullet,
				pos,
				boss.Vector2ToAngle(boss.playerPos.CurrentValue - pos)
			));
			yield return new WaitForSeconds(0.375f);
		}
	}

	private void SpawnBendingLine(Vector2 pos, Vector2 dir, int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			var obj = boss.takeBulletFunc.GetFunction()((
				FlyweightType.Lich2BendingBullet,
				pos,
				boss.Vector2ToAngle(dir)
			));
			obj.GetComponent<Lich2BendingBullet>().delayMove = 0.08f * i;
		}
	}
}

