using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IEnemyInteractable
{
	public FloatVariable maxHealth;
	public FloatVariable currentHealth;
	public float moveSpeed;
	public bool isFacingRight = true;
	public bool isStayPosCenter = false;
	public bool isMoveStatePrevious = false;
	public float heightAreaMovable = 5f;
	public float widthAreaMovable = 7f;

	public Vector2Variable PlayerPos;
	public Vector2Variable BossPos;
	public Vector2Variable PosCenterBoss;

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
	public BossElimentalerState ElimentalerState { get; set; }
	public BossThrowKunaiState ThrowKunaiState { get; set; }
	public BossDieState DieState { get; set; }
	public BossMoveToCenterState MoveToCenterState { get; set; }
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
		ElimentalerState = new BossElimentalerState(this, StateMachine);
		ThrowKunaiState = new BossThrowKunaiState(this, StateMachine);
		DieState = new BossDieState(this, StateMachine);
		MoveToCenterState = new BossMoveToCenterState(this, StateMachine);

		StateMachine.Initialize(MoveToCenterState);
	}
	private void Start()
	{
		currentHealth.CurrentValue = maxHealth.CurrentValue;
		//StartCoroutine(TestSkill());
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
	public void Die()
	{
		StopAllCoroutines();
		StateMachine.ChangeState(DieState);
	}
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

	public void UpdateAnimationByPosCenter()
	{
		Vector2 direction = PosCenterBoss.CurrentValue - (Vector2)transform.position;
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
		//StateMachine.ChangeState(SummonCheeseState);
		//StateMachine.ChangeState(ElimentalerState);
		//StateMachine.ChangeState(ThrowKunaiState);
		//StateMachine.ChangeState(RollState);
		//StateMachine.ChangeState(MoveState);
		StateMachine.ChangeState(MoveToCenterState);
	}

	public void OnEnemyBulletHit(float damge)
	{
		currentHealth.CurrentValue = currentHealth.CurrentValue - damge;
		Animator.SetBool("IsDamaged", true);

		if(currentHealth.CurrentValue <= 0)
		{
			Die();
		}
	}
	public void SetAnimationAfterHurt()
	{
		Animator.SetBool("IsDamaged", false);
	}

	public void EventOnEnterPosCenter()
	{
		Debug.Log("Enter Pos Center");
		isStayPosCenter = true;
	}

	public void EventOnExitPosCenter()
	{
		Debug.Log("Exit Pos Center");
		isStayPosCenter = false;
	}
	public bool CheckOutAreaMovable()
	{
		if (transform.position.x > PosCenterBoss.CurrentValue.x + widthAreaMovable / 2 || transform.position.x < PosCenterBoss.CurrentValue.x - widthAreaMovable / 2)
		{
			return true;
		}
		if (transform.position.y > PosCenterBoss.CurrentValue.y + heightAreaMovable / 2 || transform.position.y < PosCenterBoss.CurrentValue.y - heightAreaMovable / 2)
		{
			return true;
		}
		return false;
	}

	public void MoveToPosCenter()
	{
		Vector2 direction = (PosCenterBoss.CurrentValue - (Vector2)transform.position).normalized;
		RB.velocity = direction * moveSpeed;
	}

	public void MechanicChangeState()
	{
		// Neu out areMoveable, di chuyen ve PosCenter
		if(CheckOutAreaMovable() && isMoveStatePrevious)
		{
			StateMachine.ChangeState(MoveToCenterState);
			return;
		}

		// Neu vua tan cong xong, di chuyen ap sat player
		if (!isMoveStatePrevious)
		{
			StateMachine.ChangeState(MoveState);
			return;
		}

		// tan cong random
		RandomUseSkill();
		isMoveStatePrevious = false;
		return;
	}

	public void RandomUseSkill()
	{
		int random = Random.Range(0, 3);
		switch (random)
		{
			case 0:
				StateMachine.ChangeState(TailWhipState);
				break;
			case 1:
				StateMachine.ChangeState(ThrowKunaiState);
				break;
			case 2:
				StateMachine.ChangeState(ElimentalerState);
				break;
		}
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

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		// Draw a cube at the transform position
		Gizmos.DrawWireCube(PosCenterBoss.CurrentValue, new Vector3(widthAreaMovable, heightAreaMovable, 0));
	}
}
