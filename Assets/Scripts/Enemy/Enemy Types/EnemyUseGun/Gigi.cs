using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gigi : EnemyUseGun
{
	public GameObjectFlyweightTypeVector2FloatFuncProvider spawnBulletFunc;

	public override void Attack()
	{
		StartCoroutine(AttackCoroutine());
	}

	public override void Die()
	{
		base.Die();
		onEnemyDown.Raise(EnemyType.Gigi);
	}

	// Coroutine for the Attack after the attack animation is done
	public IEnumerator AttackCoroutine()
	{
		yield return new WaitForSeconds(0.75f);
		Vector2 direction = (PlayerPos.CurrentValue - (Vector2)transform.position).normalized;

		float angle = Vector2ToAngle(direction);

		spawnBulletFunc.GetFunction()((FlyweightType.EnemyBullet, new Vector2(transform.position.x, transform.position.y), angle));
	}
}
