using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyUseGun
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
		onEnemyDown.Raise(EnemyType.Enemy3);
	}

	// Coroutine for the Attack after the attack animation is done
	public IEnumerator AttackCoroutine()
	{
		// Cho ban dan sau 0.4s
		yield return new WaitForSeconds(0.4f);

		animatorGun.SetBool("isIdle", false);

		// Ban lien tuc 6 vien cach nhau 0.2s
		for (int i = 0; i < 2; i++)
		{
			animatorGun.SetBool("isShoot", i % 2 == 0);
			Vector2 direction = (PlayerPos.CurrentValue - (Vector2)transform.position).normalized;

			float angle = Vector2ToAngle(direction);

			Vector2 posSpawnBullet = new Vector2(transform.position.x, transform.position.y) + direction * 1f;

			spawnBulletFunc.GetFunction()((FlyweightType.EnemyBullet, posSpawnBullet, angle));
			spawnBulletFunc.GetFunction()((FlyweightType.EnemyBullet, posSpawnBullet, angle + 10f));
			spawnBulletFunc.GetFunction()((FlyweightType.EnemyBullet, posSpawnBullet, angle - 10f));
			spawnBulletFunc.GetFunction()((FlyweightType.EnemyBullet, posSpawnBullet, angle + 5f));
			spawnBulletFunc.GetFunction()((FlyweightType.EnemyBullet, posSpawnBullet, angle - 5f));
			spawnBulletFunc.GetFunction()((FlyweightType.EnemyBullet, posSpawnBullet, angle + 15f));
			spawnBulletFunc.GetFunction()((FlyweightType.EnemyBullet, posSpawnBullet, angle - 15f));

			yield return new WaitForSeconds(0.8f);
		}

		animatorGun.SetBool("isIdle", true);
	}
}
