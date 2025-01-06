using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseSlamBullet : MonoBehaviour
{
	public float speed;
	public float acceleration;
	public float maxSpeed;
	public Vector2 direction;
	public float timeRadomCanMove;
	private float timeCanMove;
	private bool canMove = false;

	public Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	void Start()
    {
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		timeCanMove = Random.Range(0, timeRadomCanMove);
		StartCoroutine(CanMove());
	}

    void Update()
    {
        
    }

	private void FixedUpdate()
	{
		Move();
	}

	public void Move()
	{
		if (!canMove) return;

		speed += acceleration * Time.fixedDeltaTime;
		if (speed > maxSpeed)
		{
			speed = maxSpeed;
		}
		rb.velocity = direction.normalized * speed;
	}

	private IEnumerator CanMove()
	{
		yield return new WaitForSeconds(timeCanMove);
		canMove = true;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		IPlayerInteractable player = collision.GetComponent<IPlayerInteractable>();
		if(player != null)
		{
			if (player.IsPlayerInteractable)
			{
				player.OnPlayerBulletHit();
				Destroy(gameObject);
			}
		}

		if(collision.gameObject.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}
	}
}
