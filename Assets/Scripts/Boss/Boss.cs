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
	public Vector2Variable BossPos;

	public FlyweightTypeVector2FloatEvent TakeBulletEvent;
	public GameObject Cheese;

	#region GetComponents
	public Rigidbody2D RB { get; set; }
	public Animator Animator;
	#endregion

	#region State Machine Variables
	public BossStateMachine StateMachine { get; set; }
	public BossIdleState IdleState { get; set; }
	public BossMoveState MoveState { get; set; }
	public BossRollState RollState { get; set; }
	public BossTailWhipState TailWhipState { get; set; }
	public BossSummonCheeseState SummonCheeseState { get; set; }
	public BossCheeseSlamState CheeseSlamState { get; set; }
	#endregion
	private void Awake()
	{
		RB = GetComponent<Rigidbody2D>();
		Animator = GetComponent<Animator>();

		StateMachine = new BossStateMachine();
		IdleState = new BossIdleState(this, StateMachine);
		MoveState = new BossMoveState(this, StateMachine);
		RollState = new BossRollState(this, StateMachine);
		TailWhipState = new BossTailWhipState(this, StateMachine);
		SummonCheeseState = new BossSummonCheeseState(this, StateMachine);
		CheeseSlamState = new BossCheeseSlamState(this, StateMachine);
		StateMachine.Initialize(IdleState);
	}
	private void Start()
	{
		StartCoroutine(TestSkill());
	}

	private void Update()
	{
		BossPos.CurrentValue = transform.position;
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

	public IEnumerator TestSkill()
	{
		yield return new WaitForSeconds(2f);
		//StateMachine.ChangeState(TailWhipState);
		StateMachine.ChangeState(SummonCheeseState);
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
