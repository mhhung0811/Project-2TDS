using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDamageEffect : IDamageEffectVisitor
{
	public BurnDamageEffect()
	{
		
	}

	public void Visit(Player player)
	{
		Debug.Log("Applying burn damage to player");
		player.OnPlayerBulletHit();
	}

	public void Visit(Enemy enemy)
	{
		Debug.Log("Applying burn damage to Enemy");
		enemy.OnEnemyBulletHit(20);
	}
}