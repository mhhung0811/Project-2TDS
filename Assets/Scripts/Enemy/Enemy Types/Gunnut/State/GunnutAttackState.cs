using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnutAttackState : EnemyState
{
	private Gunnut gunnut => (Gunnut)base.Enemy;
	public GunnutAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		Vector2 direction = (gunnut.PlayerPos.CurrentValue - (Vector2)gunnut.transform.position).normalized;
		gunnut.animator.SetBool("IsAttack", true);
		gunnut.animator.SetFloat("XInputAtk", direction.x);
		gunnut.animator.SetFloat("YInputAtk", direction.y);
		gunnut.StartCoroutine(Attack(direction));
		gunnut.StartCoroutine(SpawnMore(direction));
	}

	public override void Exit()
	{
		base.Exit();
		gunnut.animator.SetBool("IsAttack", false);
	}

	public override void FrameUpdate()
	{
		
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator Attack(Vector2 direction)
	{
		yield return new WaitForSeconds(0.5f);
		Vector2 spawnPos = (Vector2)gunnut.transform.position + direction.normalized * 0.5f;
		gunnut.SpawnArcBullets(spawnPos, direction, 90, 20);
		yield return new WaitForSeconds(0.5f);
		EnemyStateMachine.ChangeState(gunnut.moveState);
	}

	private IEnumerator SpawnMore(Vector2 direction)
	{
		yield return new WaitForSeconds(0.425f);
		Vector2 spawnPos = (Vector2)gunnut.transform.position + direction.normalized * 0.5f;
		gunnut.SpawnHeadArrowBullet(spawnPos, direction);
		
		// left
		Vector2 dirLeft = gunnut.AngleToVector2(gunnut.Vector2ToAngle(direction) - 45f);
		gunnut.SpawnHeadArrowBullet(spawnPos, dirLeft);

		// right
		Vector2 dirRight = gunnut.AngleToVector2(gunnut.Vector2ToAngle(direction) + 45f);
		gunnut.SpawnHeadArrowBullet(spawnPos, dirRight);
		yield return new WaitForSeconds(0.1f);
		gunnut.SpawnHeadArrowBullet(spawnPos, direction);
		gunnut.SpawnHeadArrowBullet(spawnPos, dirLeft);
		gunnut.SpawnHeadArrowBullet(spawnPos, dirRight);
	}
}
