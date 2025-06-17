using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestShieldState : HighPriestState
{
	private float durationState = 4f;
	private float delayAttack = 0.5f;
	public HighPriestShieldState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.StartCoroutine(LogicAnimation());
		boss.StartCoroutine(StartSkill());
		boss.StartCoroutine(StartDuration());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Shield", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator LogicAnimation()
	{
		boss.animator.SetBool("Attack", true);
		yield return new WaitForSeconds(0.5f);
		boss.animator.SetBool("Attack", false);
		boss.animator.SetBool("Shield", true);
	}

	private IEnumerator StartSkill()
	{
		float radiusStart = 2f;
		float radiusStep = 0.5f;
		int bulletCount = 12;
		float delayStart = durationState;
		yield return new WaitForSeconds(delayAttack);
		for(int i = 0; i < 4; i++)
		{
			float radiusTemp = radiusStart + radiusStep * i;
			int bulletCountTemp = bulletCount + i * 3;
			delayStart = delayStart - 0.1f * radiusTemp - 0.75f;

			Spawn2Dir(
				bulletCount: bulletCountTemp,
				radius: radiusTemp,
				delayFire: delayStart,
				clockwise: 1,
				duration: 0.1f * radiusTemp
			);
			yield return new WaitForSeconds(0.1f * radiusTemp + 0.25f);
		}
	}

	private void Spawn2Dir(int bulletCount, float radius, float delayFire, int clockwise, float duration)
	{

		boss.StartCoroutine(SpawnArcBullets(
			center: boss.transform.position,
			direction: Vector2.right,
			totalAngle: 180,
			bulletCount: bulletCount,
			clockwise: clockwise,
			durationSpawn: duration,
			delayFire: delayFire,
			rangeRadius: radius
		));

		boss.StartCoroutine(SpawnArcBullets(
			center: boss.transform.position,
			direction: Vector2.left,
			totalAngle: 180,
			bulletCount: bulletCount,
			clockwise: clockwise,
			durationSpawn: duration,
			delayFire: delayFire,
			rangeRadius: radius
		));
	}


	private IEnumerator SpawnArcBullets(Vector2 center, Vector2 direction, float totalAngle, int bulletCount, int clockwise, float durationSpawn, float delayFire, float rangeRadius)
	{
		if (clockwise >= 0)
		{
			clockwise = 1;
		}
		else
		{
			clockwise = -1;
		}

		float startAngle = -totalAngle / 2f * clockwise;
		float stepAngle = totalAngle / (bulletCount - 1);
		Vector2 pos = Vector2.zero;
		List<EnemyBulletNew> bullets = new List<EnemyBulletNew>();
		for (int i = 0; i < bulletCount; i++)
		{
			float angle = (startAngle + i * stepAngle * clockwise) + boss.Vector2ToAngle(direction);

			pos = center + boss.AngleToVector2(angle) * rangeRadius;

			var gobj = boss.takeBulletFunc.GetFunction()((
				FlyweightType.HighPriestShieldBullet,
				pos,
				angle + 180f
			));

			if (gobj)
			{
				var bullet = gobj.GetComponent<EnemyBulletNew>();
				bullets.Add(bullet);
			}

			yield return new WaitForSeconds(durationSpawn / bulletCount);
		}

		yield return new WaitForSeconds(delayFire);



		for (int i = 0; i < bulletCount; i++)
		{
			bullets[i].StartMove();
		}
	}

	private IEnumerator StartDuration()
	{
		yield return new WaitForSeconds(durationState);
		stateMachine.ChangeState(boss.idleState);
	}
}

