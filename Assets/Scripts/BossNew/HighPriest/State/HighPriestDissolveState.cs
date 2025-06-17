using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class HighPriestDissolveState : HighPriestState
{
	private float timeToDissolve = 0.25f;
	private float delayAttack = 0.3f;
	public HighPriestDissolveState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.StartCoroutine(Skill());
		boss.col.enabled = false;
		boss.enterDissolve.Raise(new Void());
		//boss.StartCoroutine(LogicAnimation());
	}

	public override void Exit()
	{
		base.Exit();
		boss.col.enabled = true;
		boss.animator.SetBool("Dissolve", false);
		boss.exitDissolve.Raise(new Void());
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator Skill()
	{
		boss.StartDissolve(1f);
		yield return new WaitForSeconds(1f);
		float time = 5;
		foreach(var pos in boss.posTele)
		{
			boss.transform.position = pos.CurrentValue;
			boss.StartAppear(timeToDissolve);
			boss.animator.SetBool("Idle", false);
			boss.animator.SetBool("Dissolve", true);
			Attack(time);
			yield return new WaitForSeconds(delayAttack);
			boss.animator.SetBool("Dissolve", false);
			boss.animator.SetBool("Idle", true);
			time -= (timeToDissolve + delayAttack);
			boss.StartDissolve(timeToDissolve);
			yield return new WaitForSeconds(timeToDissolve);
		}

		yield return new WaitForSeconds(2f);
		boss.transform.position = boss.posCenter.CurrentValue;
		boss.StartAppear(1f);
		yield return new WaitForSeconds(1f);
		stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator LogicAnimation()
	{
		yield return new WaitForSeconds(timeToDissolve);
		boss.animator.SetBool("Dissolve", true);
		yield return new WaitForSeconds(timeToDissolve*15f);
		boss.animator.SetBool("Dissolve", false);
		boss.animator.SetBool("Idle", true);
	}

	public void Attack(float delayFire)
	{
		Vector2 dir = boss.posCenter.CurrentValue - (Vector2)boss.transform.position;
		Spawn(dir, delayFire);
	}

	private void Spawn(Vector2 direction, float delayFire)
	{
		boss.StartCoroutine(SpawnArcBullets(
			center: boss.transform.position,
			direction: direction,
			totalAngle: 180,
			bulletCount: 12,
			clockwise: 1,
			totalAngleFire: 60,
			durationSpawn: 0.2f,
			delayFire: delayFire
		));
	}

	public IEnumerator SpawnArcBullets(Vector2 center, Vector2 direction, float totalAngle, int bulletCount, int clockwise, float totalAngleFire, float durationSpawn, float delayFire)
	{
		if (clockwise >= 0)
		{
			clockwise = 1;
		}
		else
		{
			clockwise = -1;
		}

		yield return new WaitForSeconds(0.1f);

		float startAngle = -totalAngle / 2f * clockwise;
		float stepAngle = totalAngle / (bulletCount - 1);
		Vector2 pos = Vector2.zero;
		List<EnemyBulletNew> bullets = new List<EnemyBulletNew>();
		for (int i = 0; i < bulletCount; i++)
		{
			float angle = (startAngle + i * stepAngle * clockwise) + boss.Vector2ToAngle(direction);

			pos = center + boss.AngleToVector2(angle) * 2f;

			var gobj = boss.takeBulletFunc.GetFunction()((
				FlyweightType.HighPriestAttackBullet,
				pos,
				angle
			));

			if (gobj)
			{
				var bullet = gobj.GetComponent<EnemyBulletNew>();
				bullets.Add(bullet);
			}

			yield return new WaitForSeconds(durationSpawn / bulletCount);
		}

		yield return new WaitForSeconds(delayFire);


		startAngle = -totalAngleFire / 2f;
		stepAngle = totalAngleFire / (bulletCount - 1);
		Vector2 dir = direction * 4f + (boss.playerPos.CurrentValue - (center + direction * 4f));
		for (int i = 0; i < bulletCount; i++)
		{
			bullets[i].SetRotate((startAngle + i * stepAngle) * clockwise + boss.Vector2ToAngle(dir));
			bullets[i].StartMove();
		}
	}
}

