using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Enemy
{
	public FlyweightTypeVector2FloatEvent spawnBulletEvent;

	public Animator animatorGun;

	public override void Attack()
	{
		StartCoroutine(AttackCoroutine());
	}

	// Coroutine for the Attack after the attack animation is done
	public IEnumerator AttackCoroutine()
	{
		// Cho ban dan sau 0.4s
		yield return new WaitForSeconds(0.4f);

		animatorGun.SetBool("isIdle", false);

		// Ban lien tuc 6 vien cach nhau 0.2s
		for (int i = 0; i < 5; i++)
		{
			animatorGun.SetBool("isShoot", i%2 == 0);
			Vector2 direction = (PlayerPos.CurrentValue - (Vector2)transform.position).normalized;

			float angle = Vector2ToAngle(direction);

			Vector2 posSpawnBullet = new Vector2(transform.position.x, transform.position.y) + direction * 1f;

			spawnBulletEvent.Raise((FlyweightType.EnemyBullet, posSpawnBullet, angle));

			yield return new WaitForSeconds(0.2f);
		}

		animatorGun.SetBool("isIdle", true);
	}
}
