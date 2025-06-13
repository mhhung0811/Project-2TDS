using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestFireState : HighPriestState
{
	private float duration = 3.5f;
	private float delay = 0.5f;
	public HighPriestFireState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{
		
	}

	public override void Enter()
	{
		base.Enter();
		boss.StartCoroutine(LogicAnimation());

		boss.StartCoroutine(CoroutineTimeOutState());

		RandomUseSkill();
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Fire", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator SkillRandom()
	{
		yield return new WaitForSeconds(delay);
		boss.StartCoroutine(CoroutineSpawnRandom(boss.fireLeft.position, 75));
		boss.StartCoroutine(CoroutineSpawnRandom(boss.fireRight.position, 75));
	}

	private void RandomUseSkill()
	{
		int random = Random.Range(0, 2);
		if(random == 0)
		{
			boss.StartCoroutine(SkillRandom());
		}
		else
		{
			boss.StartCoroutine(SkillFireCircle());
		}
	}

	private IEnumerator LogicAnimation()
	{
		boss.animator.SetBool("Attack", true);
		yield return new WaitForSeconds(0.5f);
		boss.animator.SetBool("Attack", false);
		boss.animator.SetBool("Fire", true);
	}

	private void SpawnBulletDirRandom(Vector2 pos)
	{
		float angle = Random.Range(0f, 360f);
		boss.takeBulletFunc.GetFunction()((FlyweightType.EnemyBullet, pos, angle));
	}

	private IEnumerator CoroutineTimeOutState()
	{
		yield return new WaitForSeconds(duration);
		stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator CoroutineSpawnRandom(Vector2 pos, int amountBullet)
	{
		float time = duration / amountBullet;
		for (int i = 0; i < amountBullet; i++)
		{
			SpawnBulletDirRandom(pos);
			yield return new WaitForSeconds(time);
		}
	}

	private IEnumerator SkillFireCircle()
	{
		yield return new WaitForSeconds(delay);
		boss.StartCoroutine(SpawnCircle(boss.fireLeft.position, 180f, true, 2, 75));
		boss.StartCoroutine(SpawnCircle(boss.fireRight.position, 0f, false, 2, 75));
	}

	private IEnumerator SpawnCircle(Vector2 pos, float angleStart, bool dir, int loopCount, int amountBullet)
	{
		float angleStep = 360f * loopCount / amountBullet;
		float angle = angleStart;
		float time = duration / amountBullet;
		for (int i = 0; i < amountBullet; i++)
		{
			if (dir)
			{
				angle += angleStep;
			}
			else { 
				angle -= angleStep;
			}

			boss.takeBulletFunc.GetFunction()((FlyweightType.EnemyBullet, pos, angle));

			yield return new WaitForSeconds(time);
		}
	}
}

