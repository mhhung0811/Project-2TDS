using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WallMongerMoveState : WallMongerState
{
	private float timeExitState = 5f;
	private Vector2 moveDirection;
	public WallMongerMoveState(WallMonger boss, WallMongerStateMachine stateMachine) : base(boss, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Move", true);
		boss.vfx.PlayVFXAttack();

		moveDirection = Vector2.down * boss.moveSpeed;

		// Start the combat sequence
		boss.StartCoroutine(Combat());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Move", false);
		boss.vfx.ExitVFXAttack();

		moveDirection = Vector2.zero;
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();

		boss.rb.MovePosition(
			boss.rb.position + moveDirection * Time.fixedDeltaTime
		);
	}

	private IEnumerator Combat()
	{
		boss.StartCoroutine(Skill1());
		yield return new WaitForSeconds(2.5f);
		boss.StartCoroutine(Skill2());
		yield return new WaitForSeconds(2.5f);
		boss.StartCoroutine(Skill1(true));
		yield return new WaitForSeconds(2.5f);
		boss.StartCoroutine(Skill2());
		yield return new WaitForSeconds(2.5f);

		stateMachine.ChangeState(boss.jumpState);
	}
	private IEnumerator Skill1(bool reverse = false)
	{
		//List<Transform> spawnPoints = Shuffle(boss.listSpawnPos);
		List<Transform> spawnPoints = boss.listSpawnPos;
		if(reverse)
		{
			spawnPoints.Reverse();
		}

		foreach (Transform pos in spawnPoints)
		{
			boss.SpawnArcBullets(pos.position, Vector2.down, 50, 6);
			yield return new WaitForSeconds(0.25f);
		}
	}

	private IEnumerator Skill2()
	{
		List<Transform> spawnPoints = Shuffle(boss.listSpawnPos);
		foreach(Transform pos in spawnPoints)
		{
			boss.StartCoroutine(SpawnBulletSKill2(pos.position));
			yield return new WaitForSeconds(0.25f);
		}
	}

	private IEnumerator SpawnBulletSKill2(Vector2 pos)
	{
		for (int i = 0; i < 5; i++)
		{
			boss.SpawnArcBullets(pos, Vector2.down, 8, 3);
			yield return new WaitForSeconds(0.15f);
		}
	}

	

	public List<Transform> Shuffle(List<Transform> source)
	{
		List<Transform> copy = new List<Transform>(source);

		for (int i = copy.Count - 1; i > 0; i--)
		{
			int j = Random.Range(0, i + 1);
			Transform temp = copy[i];
			copy[i] = copy[j];
			copy[j] = temp;
		}

		return copy;
	}
}
