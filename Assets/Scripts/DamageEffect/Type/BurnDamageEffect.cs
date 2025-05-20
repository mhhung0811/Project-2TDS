using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDamageEffect : IDamageEffectVisitor
{
	public void Visit(Player player)
	{
		// Implement the logic for applying burn damage to the player
		Debug.Log("Applying burn damage to player");
	}

	public void Visit(Enemy enemy)
	{
		// Implement the logic for applying burn damage to the enemy
		Debug.Log("Applying burn damage to enemy");
	}
}