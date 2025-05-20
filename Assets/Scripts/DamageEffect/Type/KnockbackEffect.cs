using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : IDamageEffectVisitor
{
	public float force;
	public Vector2 sourcePos;

	public KnockbackEffect(float force, Vector2 direction)
	{
		this.force = force;
		this.sourcePos = direction;
	}
	public void Visit(Player player)
	{
		Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
		Vector2 direction = (rb.position - sourcePos).normalized;
		rb.velocity = Vector2.zero;
		rb.drag = 5f;
		rb.AddForce(direction * force, ForceMode2D.Impulse);
	}

	public void Visit(Enemy enemy)
	{
		Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
		Vector2 direction = (rb.position - sourcePos).normalized;
		rb.velocity = Vector2.zero;
		rb.drag = 5f;
		rb.AddForce(direction * force, ForceMode2D.Impulse);
	}
}
