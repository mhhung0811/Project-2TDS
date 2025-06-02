using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplode : MonoBehaviour, IEnemyInteractable, IExplodedInteractable
{
    public float radius;
	public float damage;
	private Animator animator;
	public bool IsEnemyInteractable { get; set; } = true;
	public bool CanExplodeInteractable { get; set; } = true;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		CanExplodeInteractable = true;
	}
	void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnEnemyBulletHit(float damge)
    {
		SoundManager.Instance.PlaySound("Explode");
		StartCoroutine(SoundExplodeDelay(0.1f));
		IsEnemyInteractable = false;
		Explode();
    }

    public void Explode()
    {
		animator.SetBool("IsExplode", true);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

		foreach (Collider2D col in colliders)
		{
			if(col.TryGetComponent<IExplodedInteractable>(out var obj))
			{
				if (obj.CanExplodeInteractable == true)
				{
					obj.OnExplode(damage);
				}
			}

			if (col.TryGetComponent<IDamageEffectApplicable>(out var dea))
			{
				dea.Accept(new KnockbackEffect(12, (Vector2)this.transform.position));
			}
		}

		CanExplodeInteractable = false;
	}

	public void OnExplode(float damage)
	{
		StartCoroutine(SoundExplodeDelay(0.1f));
		StartCoroutine(ExplodeDelay(0.1f));
	}
	
	private IEnumerator ExplodeDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		Explode();
	}


	public void DestroyBarrel()
	{
		Destroy(gameObject);
	}

	public IEnumerator SoundExplodeDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		SoundManager.Instance.PlaySound("Explode");	
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
