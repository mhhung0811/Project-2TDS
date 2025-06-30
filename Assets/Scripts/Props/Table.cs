using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TableState
{
	Idle,
	Up,
	Down,
	Right,
	Left,
	Break,
}

public class Table : MonoBehaviour, IEnemyInteractable, IPlayerInteractable, IInteractable
{
	public TableState curState;
	public int maxHealth = 12;
	public int currentHealth;
	public HintInteract hintInteract;

	private BoxCollider2D col;
	private Animator animator;
	private Rigidbody2D rb;
	public bool isInteractable { get; set; }
	public bool IsEnemyInteractable { get; set; }
	public bool CanRollThrough { get; private set; }

	public bool IsPlayerInteractable { get; set; }
	private void Awake()
	{
		col = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		CanRollThrough = false;
	}

	private void Start()
	{
		currentHealth = maxHealth;
		curState = TableState.Idle;
		IsEnemyInteractable = false;
		ApplyState(curState);
	}

	void Update()
	{
		
	}

	public void SetState(TableState newState)
	{
		if (newState == curState) return;

		ExitState();
		curState = newState;
		ApplyState(curState);
	}

	private void ApplyState(TableState state)
	{
		switch (state)
		{
			case TableState.Idle:
				animator.SetBool("Idle", true);
				col.offset = new Vector2(0, -0.2f);
				col.size = new Vector2(2.6f, 1.6f);
				CanRollThrough = true;
				rb.bodyType = RigidbodyType2D.Static;
				IsEnemyInteractable = false;
				IsPlayerInteractable = false;
				currentHealth = maxHealth;
				hintInteract.canInteract = true;
				break;

			case TableState.Up:
				animator.SetBool("Up", true);
				col.offset = new Vector2(0, -0.3f);
				col.size = new Vector2(2.6f, 1.5f);
				SetShield();
				break;

			case TableState.Left:
				animator.SetBool("Left", true);
				col.offset = new Vector2(-1.15f, 0);
				col.size = new Vector2(0.4f, 2.6f);
				SetShield();
				break;

			case TableState.Right:
				animator.SetBool("Right", true);
				col.offset = new Vector2(1.15f, 0);
				col.size = new Vector2(0.4f, 2.6f);
				SetShield();
				break;

			case TableState.Down:
				animator.SetBool("Down", true);
				col.offset = new Vector2(0, -0.8f);
				col.size = new Vector2(2.6f, 1.1f);
				SetShield();
				break;

			case TableState.Break:
				animator.SetBool("Break", true);
				col.enabled = false;
				IsEnemyInteractable = false;
				rb.bodyType = RigidbodyType2D.Static;
				break;
		}
	}

	private void ExitState()
	{
		switch(curState)
		{
			case TableState.Idle:
				animator.SetBool("Idle", false);
				hintInteract.OffInteract();
				break;
			case TableState.Up:
				animator.SetBool("Up", false);
				break;
			case TableState.Left:
				animator.SetBool("Left", false);
				break;
			case TableState.Right:
				animator.SetBool("Right", false);
				break;
			case TableState.Down:
				animator.SetBool("Down", false);
				break;
			case TableState.Break:
				animator.SetBool("Break", false);
				col.enabled = true;
				break;
		}
	}

	public void SetShield()
	{
		col.enabled = true;
		CanRollThrough = false;
		rb.bodyType = RigidbodyType2D.Dynamic;
		IsEnemyInteractable = true;
		IsPlayerInteractable = true;
	}

	public void OnEnemyBulletHit(float damge)
	{
		currentHealth -= 1;

		if(currentHealth <= 0)
		{
			SetState(TableState.Break);
			IsEnemyInteractable = false;
			return;
		}
	}
	public void OnPlayerBulletHit()
	{
		currentHealth -= 1;

		if (currentHealth <= 0)
		{
			SetState(TableState.Break);
			IsPlayerInteractable = false;
			return;
		}
	}

	public void Interact(GameObject go)
	{
		Vector2 direction = go.transform.position - this.transform.position;
		float angle = Vector2ToAngle(direction);

		if(angle >= 30 && angle <=150)
		{
			SetState(TableState.Down);
		}
		else if (angle >= 150 && angle <= 210)
		{
			SetState(TableState.Right);
		}
		else if (angle >= 210 && angle <= 330)
		{
			SetState(TableState.Up);
		}
		else
		{
			SetState(TableState.Left);
		}
	}

	public float Vector2ToAngle(Vector2 direction)
	{
		float angleInRadians = Mathf.Atan2(direction.y, direction.x);

		float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

		if (angleInDegrees < 0)
		{
			angleInDegrees += 360f;
		}

		return angleInDegrees;
	}
}

