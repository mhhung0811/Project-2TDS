using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestAttackState : HighPriestState
{
	private float duration = 0.5f/0.6f;
	private float durationSpawn = 0.15f;
	private float delayFire = 0.35f;
	public int accumulatedCombo = 0;
	public HighPriestAttackState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		accumulatedCombo++;
		if(accumulatedCombo >= 3)
		{
			boss.StartCoroutine(ComboSkill());
			accumulatedCombo = 0;
		}
		else
		{
			boss.StartCoroutine(StartDuration());
			ControllerUseSkill();
		}
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Attack", false);
		boss.animator.SetBool("AttackLeft", false);
		boss.animator.SetBool("AttackRight", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private void SpawnRight()
	{
		boss.StartCoroutine(SpawnArcBullets(
			center: boss.transform.position, 
			direction: Vector2.right, 
			totalAngle: 180, 
			bulletCount: 12, 
			clockwise: 1,
			totalAngleFire: 60
		));
	}

	private void SpawnLeft()
	{
		boss.StartCoroutine(SpawnArcBullets(
			center: boss.transform.position,
			direction: Vector2.left,
			totalAngle: 180,
			bulletCount: 12,
			clockwise: -1,
			totalAngleFire: 60
		));
	}

	private void ControllerUseSkill()
	{
		int rand = Random.Range(0, 2);
		if (rand == 0)
		{
			Attack();
		}
		else
		{
			if (boss.playerPos.CurrentValue.x > boss.transform.position.x)
			{
				AttackRight();
			}
			else
			{
				AttackLeft();
			}
		}
	}

	private IEnumerator ComboSkill()
	{
		boss.animator.SetBool("AttackLeft", true);
		SpawnLeft();
		yield return new WaitForSeconds(duration);
		boss.animator.SetBool("AttackRight", true);
		SpawnRight();
		yield return new WaitForSeconds(duration);
		boss.animator.SetBool("Attack", true);
		SpawnLeft();
		SpawnRight();
		yield return new WaitForSeconds(duration);
		stateMachine.ChangeState(boss.idleState);
	}

	private void Attack()
	{
		boss.animator.SetBool("Attack", true);
		SpawnLeft();
		SpawnRight();
	}

	private void AttackLeft()
	{
		boss.animator.SetBool("AttackLeft", true);
		SpawnLeft();
	}

	private void AttackRight()
	{
		boss.animator.SetBool("AttackRight", true);
		SpawnRight();
	}

	public IEnumerator SpawnArcBullets(Vector2 center, Vector2 direction, float totalAngle, int bulletCount, int clockwise,float totalAngleFire)
	{
		if(clockwise >= 0)
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
			
			pos = center + boss.AngleToVector2(angle) * 3f;

			var gobj = boss.takeBulletFunc.GetFunction()((
				FlyweightType.HighPriestAttackBullet,
				pos,
				angle
			));

			if(gobj)
			{
				var bullet = gobj.GetComponent<EnemyBulletNew>();
				bullets.Add(bullet);
			}

			yield return new WaitForSeconds(durationSpawn/bulletCount);
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

	private IEnumerator StartDuration()
	{
		yield return new WaitForSeconds(duration);
		stateMachine.ChangeState(boss.idleState);
	}
}

