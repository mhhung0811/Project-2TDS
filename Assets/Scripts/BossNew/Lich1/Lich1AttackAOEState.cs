using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Lich1AttackAOEState : Lich1State
{
	private float attackDuration = 12f;
	public Lich1AttackAOEState(Lich1 highPriest, Lich1StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("AttackAOE", true);
		boss.StartCoroutine(CoolDownAttackAOEState());
		boss.StartCoroutine(Skill1());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("AttackAOE", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator CoolDownAttackAOEState()
	{
		yield return new WaitForSeconds(attackDuration);
		boss.stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator Skill1()
	{
		int clockWise = 1;
		float angleStart = 0f;
		int amountWise = 6;
		float angleStep = 360f / amountWise;

		float currentOffset = 0f;
		float targetOffset = 7f;
		float smoothSpeed = 10f; // tốc độ mượt

		for (int i = 0; i < 100; i++)
		{
			if ((i + 1) % 20 == 0)
			{
				clockWise *= -1;
				targetOffset *= clockWise; // đổi hướng mượt
			}

			// Nội suy dần cho smooth transition
			currentOffset = Mathf.Lerp(currentOffset, targetOffset, Time.deltaTime * smoothSpeed);

			for (int j = 0; j < amountWise; j++)
			{
				boss.takeBulletFunc.GetFunction()((
					FlyweightType.EnemyBullet,
					(Vector2)boss.posGun.transform.position,
					angleStart + angleStep * j
				));
			}

			angleStart += currentOffset;
			yield return new WaitForSeconds(0.1f);
		}
	}

}

