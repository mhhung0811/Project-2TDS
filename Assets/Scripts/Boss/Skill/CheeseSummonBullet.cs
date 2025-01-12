using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseSummonBullet : MonoBehaviour
{
    public float speed;
    public float acceleration;
	public float maxSpeed;
	public Rigidbody2D rb;
	public Animator animator;
	public Vector2 direction;
	private bool canMove = false;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	void Start()
    {
		animator.SetFloat("XInput", -direction.x);
		animator.SetFloat("YInput", -direction.y);
	}

    void Update()
    {
        
    }

	private void FixedUpdate()
	{
		Move();
	}

	private void OnEnable()
	{
		speed = Random.Range(-0.2f, 0f);
		StartCoroutine(CanMove());
	}

	private void OnDisable()
	{
	}

	public void Move()
    {
		if(!canMove)
		{
			return;
		}

		speed += acceleration * Time.fixedDeltaTime;
		if(speed > maxSpeed)
		{
			speed = maxSpeed;
		}
		rb.velocity = direction * speed;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		IPlayerInteractable player = collision.GetComponent<IPlayerInteractable>();
		if (player != null)
		{
			if (player.IsPlayerInteractable)
			{
				player.OnPlayerBulletHit();
				Destroy(gameObject);
			}
		}
	}

	public IEnumerator CanMove()
	{
		yield return new WaitForSeconds(0.5f);
		canMove = true;
	}
}
