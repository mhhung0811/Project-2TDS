using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerJumpState : WallMongerState
{
	private float width = 16f;
	private int bulletCount = 24;
	public WallMongerJumpState(WallMonger boss, WallMongerStateMachine stateMachine) : base(boss, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Jump", true);

		boss.StartCoroutine(DelayAttack());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Jump", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private void Attack()
	{
		// Spawn bullet
		float angle = boss.Vector2ToAngle(Vector2.down);
		Vector2 posStart = new Vector2(boss.transform.position.x - 8.0f, boss.transform.position.y - 1.8f);
		float offsetX = width / (bulletCount - 1);
		for (int i =0;i < bulletCount; i++)
		{
			Vector2 posSpawn = posStart + Vector2.right * (i * offsetX);
			boss.takeBulletEvent.Raise((FlyweightType.EnemyBullet, posSpawn, angle));

			EffectManager.Instance.PlayEffect(EffectType.EfBulletCollide , posSpawn, Quaternion.identity);
		}
	}

	private IEnumerator DelayAttack()
	{
		yield return new WaitForSeconds(1.1f);
		for (int i = 0; i < 4; i++)
		{
			Attack();
			yield return new WaitForSeconds(0.15f);
		}
		stateMachine.ChangeState(boss.idleState);
	}
}
