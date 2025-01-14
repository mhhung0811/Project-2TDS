using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplode : MonoBehaviour, IEnemyInteractable
{
    public float radius;
	private Animator animator;
	public bool IsEnemyInteractable { get; set; } = true;

	private void Awake()
	{
		animator = GetComponent<Animator>();
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

		foreach (Collider2D nearbyObject in colliders)
		{
			IExplodedInteractable enemyInteractable = nearbyObject.GetComponent<IExplodedInteractable>();
			if (enemyInteractable != null)
			{
				if (enemyInteractable.IsExplodedInteractable)
				{
					enemyInteractable.OnExplode();
					Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
					if (rb != null)
					{
						Vector2 direction = (rb.position - (Vector2)transform.position).normalized;
						rb.velocity = Vector2.zero;
						rb.drag = 5f;
						rb.AddForce(direction * 12, ForceMode2D.Impulse);
					}
				}
			}
		}
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
