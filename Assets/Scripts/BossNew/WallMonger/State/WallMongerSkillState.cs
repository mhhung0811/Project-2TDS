using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerSkillState : WallMongerState
{
	public WallMongerSkillState(WallMonger boss, WallMongerStateMachine stateMachine) : base(boss, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Skill", true);

		boss.StartCoroutine(Combat());
		boss.StartCoroutine(CoroutineChangeState());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Skill", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator Combat()
	{
		yield return new WaitForSeconds(0.5f);
		boss.StartCoroutine(Skill1());
	}

	private IEnumerator CoroutineChangeState()
	{
		yield return new WaitForSeconds(1f);
		boss.animator.SetBool("Skill", false);
		boss.animator.SetBool("Idle", true);
		yield return new WaitForSeconds(2f);
		stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator Skill1() // Bắn 4 lượt đạn thẳng hàng bến trái rồi sang bên phải
	{
		yield return new WaitForSeconds(0.5f);
		int bulletCount = 10;
		float width = 7;

		// left
		Vector2 posStartL = new Vector2(boss.transform.position.x - 8.0f, boss.transform.position.y - 2f);
		for (int i = 0; i < 4; i++)
		{
			boss.SpawnABulletLine(posStartL, bulletCount, width);
			yield return new WaitForSeconds(0.15f);
		}

		// right
		yield return new WaitForSeconds(0.15f);
		Vector2 posStartR = new Vector2(boss.transform.position.x + 1, boss.transform.position.y - 2f);
		for (int i = 0; i < 4; i++)
		{
			boss.SpawnABulletLine(posStartR, bulletCount, width);
			yield return new WaitForSeconds(0.15f);
		}

		// left
		yield return new WaitForSeconds(0.15f);
		posStartL = new Vector2(boss.transform.position.x - 8.0f, boss.transform.position.y - 2f);
		for (int i = 0; i < 4; i++)
		{
			boss.SpawnABulletLine(posStartL, bulletCount, width);
			yield return new WaitForSeconds(0.15f);
		}

		// right
		yield return new WaitForSeconds(0.15f);
		posStartR = new Vector2(boss.transform.position.x + 1, boss.transform.position.y - 2f);
		for (int i = 0; i < 4; i++)
		{
			boss.SpawnABulletLine(posStartR, bulletCount, width);
			yield return new WaitForSeconds(0.15f);
		}
	}
}
