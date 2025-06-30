using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBombShee : MonoBehaviour
{
    public LayerMask bulletPlayer;
    public float radius;

    private Animator ani;
	private SpriteRenderer spriteRenderer;
	private bool isScreeching = false;
	private void Awake()
	{
		ani = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

    void Start()
    {
        
    }

    void Update()
    {
        if(isScreeching)
		{
			DestroyBulletPlayerInRange();
		}
	}

    private void DestroyBulletPlayerInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, bulletPlayer);
		foreach (Collider2D collider in colliders)
		{
			Projectile projectile = collider.GetComponent<Projectile>();
            if(projectile != null)
            {
                projectile.settings.flyweightFunc.GetFunction()(projectile);
			}
		}
	}

	private void OnEnable()
	{
		isScreeching = false;
		
		ani.SetBool("IsStart", false);
		ani.SetBool("IsLoop", false);
	}

	private void OnDisable()
	{
		spriteRenderer.sprite = null; // Reset sprite to avoid flickering
	}

	public void StartScreech()
    {
        ani.SetBool("IsStart", true);
        isScreeching = true;
	}

    public void LoopScreech()
    {
		ani.SetBool("IsLoop", true);
		isScreeching = true;
	}

    public void EndScreech()
    {
		ani.SetBool("IsStart", false);
		ani.SetBool("IsLoop", false);
        isScreeching = false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
