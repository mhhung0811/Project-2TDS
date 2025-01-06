using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
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
		animatorGun.SetBool("isShoot", true);

		Vector2 direction = (PlayerPos.CurrentValue - (Vector2)transform.position).normalized;
		float angle = Vector2ToAngle(direction);
		Vector2 posSpawnBullet = new Vector2(transform.position.x, transform.position.y) + direction * 1f;
		Vector2 posSpawnBullet1 = posSpawnBullet + new Vector2(direction.x, direction.y) * 0.25f;

		spawnBulletEvent.Raise((FlyweightType.EnemySniperBullet, posSpawnBullet1, angle));

		yield return new WaitForSeconds(0.9f);


		animatorGun.SetBool("isIdle", true);
	}
}
