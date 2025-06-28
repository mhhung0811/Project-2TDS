using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Lich1AttackAOEState : Lich1State
{
	private float delayAttack = 1/6f;
	private bool isSkill1 = true;
	public Lich1AttackAOEState(Lich1 highPriest, Lich1StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("AttackAOE", true);
		if (isSkill1)
		{
			boss.StartCoroutine(Skill1());
			isSkill1 = false;
		}
		else
		{
			boss.StartCoroutine(Skill2());
			isSkill1 = true;
		}

		boss.vfx.SetActive(true);
		boss.vfx.transform.position = boss.posAOE.transform.position;
		boss.vfx.GetComponent<Animator>().SetBool("AttackAOE", true);
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("AttackAOE", false);
		boss.vfx.GetComponent<Animator>().SetBool("AttackAOE", false);
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

		int clockWise = 1;
		float angleStart = 0f;
		int amountWise = 7;
		float angleStep = 360f / amountWise;

		float currentOffset = 0f;
		float targetOffset = 8f;
		float smoothSpeed = 10f; // tốc độ mượt

		for (int i = 0; i < 100; i++)
		{
			if ((i + 1) % 20 == 0)
			{
				clockWise *= -1;
				targetOffset = 8 * clockWise; // đổi hướng mượt
			}

			// Nội suy dần cho smooth transition
			currentOffset = Mathf.Lerp(currentOffset, targetOffset, Time.deltaTime * smoothSpeed);

			for (int j = 0; j < amountWise; j++)
			{
				boss.takeBulletFunc.GetFunction()((
					FlyweightType.EnemyBullet,
					(Vector2)boss.posAOE.transform.position + boss.AngleToVector2(angleStart + angleStep * j).normalized * 0.75f,
					angleStart + angleStep * j
				));
			}

			angleStart += currentOffset;
			yield return new WaitForSeconds(0.1f);
		}

		boss.stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator Skill2()
	{
		yield return new WaitForSeconds(delayAttack);

		for (int i = 0;i < 6; i++)
		{
			boss.SpawnArcBullets(
				boss.posAOE.transform.position,
				Vector2.right,
				360,
				36,
				FlyweightType.EnemyBullet
			);
			yield return new WaitForSeconds(0.15f);

			boss.SpawnArcBullets(
				boss.posAOE.transform.position,
				Vector2.right,
				360,
				36,
				FlyweightType.EnemyBullet
			);
			yield return new WaitForSeconds(0.6f);
			boss.SpawnArcBullets(
				boss.posAOE.transform.position,
				boss.AngleToVector2(10*i + 5),
				360,
				18,
				FlyweightType.LichGunBullet
			);
			yield return new WaitForSeconds(0.6f);
		}

		boss.stateMachine.ChangeState(boss.idleState);
	}
}

