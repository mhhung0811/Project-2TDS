using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : BombBase, IPlayerItem
{
	public float force = 12f;
	public float damage = 50f;
	private void Awake()
	{
		radius = 2.0f;
		force = 12f;
		damage = 50f;
	}
	void Update()
	{
        if (isThrowing)
        {
            Throwing();
        }
	}
	public override void Throw(Vector2 pos)
    {
        isThrowing = true;
        basePos = this.transform.position;
		direction = (pos - basePos);
        verticalSpeed = direction.magnitude * 0.5f;
        float time = 2 * (-verticalSpeed / gravity);
		horizontalSpeed = direction.magnitude / time;
        direction = direction.normalized;
	}

    public override void Explode()
    {
		SoundManager.Instance.PlaySound("Explode");
		EffectManager.Instance.PlayEffect(EffectType.EfExplode, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);

		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
		foreach (Collider2D col in colliders)
		{
			if (col.TryGetComponent<IExplodedInteractable>(out var obj))
			{
				if (obj.CanExplodeInteractable == true)
				{
					obj.OnExplode(damage);
				}
			}

			if (col.TryGetComponent<IDamageEffectApplicable>(out var dea))
			{
				dea.Accept(new KnockbackEffect(force, (Vector2)this.transform.position));
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}

	#region IPlayerItem Implementation

	public int manaCost { get; set; }
	public float cooldown { get; set; }

	public void UseItem(Vector2 pos)
	{
		Throw(pos);
	}

	#endregion
}
