using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyUseGun
{
	public GameObjectFlyweightTypeVector2FloatFuncProvider spawnBulletFunc;

	public Animator animatorGun;

	public override void Attack()
	{
		StartCoroutine(AttackCoroutine());
	}

	public override void Die()
	{
		base.Die();
		onEnemyDown.Raise(EnemyType.Enemy2);
	}

	// Coroutine for the Attack after the attack animation is done
	public IEnumerator AttackCoroutine()
	{
		// Cho ban dan sau 0.4s
		yield return new WaitForSeconds(0.4f);

		animatorGun.SetBool("isIdle", false);
		animatorGun.SetBool("isShoot", true);

		Vector2 direction = (PlayerPos.CurrentValue - (Vector2)transform.position).normalized;
		float angle = Vector2ToAngle(direction);
		Vector2 posSpawnBullet = new Vector2(transform.position.x, transform.position.y) + direction * 1f;
		Vector2 posSpawnBullet1 = posSpawnBullet + new Vector2(direction.x, direction.y) * 0.25f;
		Vector2 posSpawnBullet2 = posSpawnBullet + new Vector2(-direction.y, direction.x) * 0.25f;
		Vector2 posSpawnBullet3 = posSpawnBullet - new Vector2(-direction.y, direction.x) * 0.25f;
		Vector2 posSpawnBullet4 = posSpawnBullet - new Vector2(direction.x, direction.y) * 0.25f;


		spawnBulletFunc.GetFunction()((FlyweightType.EnemyBullet, posSpawnBullet1, angle));
		spawnBulletFunc.GetFunction()((FlyweightType.EnemyBullet, posSpawnBullet2, angle - 10f));
		spawnBulletFunc.GetFunction()((FlyweightType.EnemyBullet, posSpawnBullet3, angle + 10f));
		spawnBulletFunc.GetFunction()((FlyweightType.EnemyBullet, posSpawnBullet4, angle));

		yield return new WaitForSeconds(0.2f);
		

		animatorGun.SetBool("isIdle", true);
	}
}
