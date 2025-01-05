using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public float maxHealth;
	public float currentHealth;
	public float moveSpeed;
	public bool isFacingRight = true;

	public Vector2Variable PlayerPos;

	#region GetComponents
	public Rigidbody2D RB { get; set; }
	public Animator Animator;
	#endregion

	#region State Machine Variables
	public BossStateMachine StateMachine { get; set; }
	public BossIdleState IdleState { get; set; }
	public BossMoveState MoveState { get; set; }
	public BossRollState RollState { get; set; }
	#endregion
	private void Awake()
	{
		RB = GetComponent<Rigidbody2D>();
		Animator = GetComponent<Animator>();

		StateMachine = new BossStateMachine();
		IdleState = new BossIdleState(this, StateMachine);
		MoveState = new BossMoveState(this, StateMachine);
		RollState = new BossRollState(this, StateMachine);
		StateMachine.Initialize(MoveState);
	}
	private void Start()
	{
		
	}

	private void Update()
	{
		StateMachine.CurrentState.FrameUpdate();
	}

	private void FixedUpdate()
	{
		StateMachine.CurrentState.PhysicsUpdate();
	}

	#region Functions
	public void Moving()
	{
		Vector2 direction = (PlayerPos.CurrentValue - (Vector2)transform.position).normalized;
		RB.velocity = direction * moveSpeed;
	}

	public void UpdateAnimationByPosPlayer()
	{
		Vector2 direction = PlayerPos.CurrentValue - (Vector2)transform.position;
		Animator.SetFloat("XInput", direction.x);
		Animator.SetFloat("YInput", direction.y);

		// Check Flip
		if (direction.x > 0 && !isFacingRight)
		{
			Flip();
		}
		else if (direction.x < 0 && isFacingRight)
		{
			Flip();
		}
	}

	public void Flip()
	{
		isFacingRight = !isFacingRight;
		transform.Rotate(0f, 180f, 0f);
	}
	#endregion


	#region Animation Triggers
	private void AnimationTriggerEvent(AnimationTriggerType triggerType)
	{
		StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
	}
	public enum AnimationTriggerType
	{
		EnemyDamaged,
		PlayFootStepSound,
	}
	#endregion
}
