using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool isFlying = false;
	public Animator animator;
	public Rigidbody2D rb;
	public float HorOrVer;
	public Collider2D collision;
	public Vector2 direction;
	public float timeDestroy;
	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		collision = GetComponent<Collider2D>();
	}

	void Start()
    {
		// Random 0 or 1
		HorOrVer = Random.Range(0, 2);
		isFlying = true;
		animator.SetFloat("HorOrVer", HorOrVer);
		timeDestroy = 15f;
	}

    // Update is called once per frame
    void Update()
    {
        if(timeDestroy > 0)
		{
			AutoDestroy();
		}
	}

	public void AutoDestroy()
	{
		timeDestroy -= Time.deltaTime;
		if(timeDestroy <= 0)
		{
			animator.SetBool("IsIdle", false);
			animator.SetBool("IsAttack", true);
		}
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (isFlying)
			return;

		IPlayerInteractable player = collision.GetComponent<IPlayerInteractable>();
		if (player != null)
		{
			if (player.IsPlayerInteractable)
			{
				player.OnPlayerBulletHit();
				animator.SetBool("IsIdle", false);
				animator.SetBool("IsAttack", true);
				this.collision.enabled = false;
				EffectManager.Instance.PlayEffect(EffectType.TrapVFX, transform.position, Quaternion.identity);
			}
		}
	}

	public void Destroy()
	{
		this.gameObject.SetActive(false);
	}
	
	public void StartFly()
	{
		StartCoroutine(Fly());
	}

	public void AnimationSetIdle()
	{
		animator.SetBool("IsLand", false);
		animator.SetBool("IsIdle", true);
		isFlying = false;
	}

	public IEnumerator Fly()
	{
		animator.SetBool("IsFly", true);
		rb.velocity = direction;
		yield return new WaitForSeconds(1f);
		rb.velocity = Vector2.zero;
		animator.SetBool("IsFly", false);
		animator.SetBool("IsLand", true);
	}
}
