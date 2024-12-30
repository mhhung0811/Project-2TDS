using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMove, ITriggerCheckable
{
    [field : SerializeField] public int MaxHealth { get; set; } = 6;
	[field: SerializeField] public int CurrentHealth { get; set; }
    public bool IsFacingRight { get; set; } = true;
	[field: SerializeField] public bool IsWithinStrikingDistance { get; set; }
    [field: SerializeField] public float AttackRange { get; set; } = 5f;
	[field: SerializeField] public float MoveSpeed { get; set; }
	[field: SerializeField] public LayerMask Obstacles { get; set; }
	[field: SerializeField] public Vector2Variable PlayerPos { get; set; }
	public Rigidbody2D RB { get; set; }
	public Animator _animator;

	[field: SerializeField] public float AttackDuration { get; set; }
    [field: SerializeField] public float AttackCooldown { get; set; }
    public float attackCooldownTimer;

	#region State Machine Variables
	public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
	#endregion

	#region Idel Variables

	#endregion
	private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
	}

    private void Start()
    {
		CurrentHealth = MaxHealth;
		RB = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
        CheckForFlip();
        UpdateAttackCoolDown();
	}

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void Damage(float damage)
    {
    }

    public void Die()
    {
    }

    public virtual void Attack()
    {

    }

    public void UpdateAttackCoolDown()
    {
        if (attackCooldownTimer < AttackCooldown)
        {
			attackCooldownTimer += Time.deltaTime;
		}
    }

    public bool CheckFinishAttackCoolDown()
    {
        if(attackCooldownTimer >= AttackCooldown)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

    public void CheckForFlip()
    {
	    if (RB.velocity.x != 0)
	    {
		    if (RB.velocity.x > 0 && !IsFacingRight)
		    {
			    Flip();
		    }
		    else if (RB.velocity.x < 0 && IsFacingRight)
		    {
			    Flip();
		    }
	    }
	    else
	    {
		    if (PlayerPos.CurrentValue.x > transform.position.x && !IsFacingRight)
		    {
			    Flip();
		    }
		    else if (PlayerPos.CurrentValue.x < transform.position.x && IsFacingRight)
		    {
			    Flip();
		    }
	    }
    }

    public void Flip()
    {
	    IsFacingRight = !IsFacingRight;
	    transform.Rotate(0f, 180f, 0f);
    }
    
    public virtual void MoveEnemy(Vector2 velocity)
    {
        RB.velocity = velocity;
        CheckForLeftOrRightFacing(velocity);
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


	public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if(IsFacingRight && velocity.x < 0)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
        else if (!IsFacingRight && velocity.x > 0)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }

	public virtual void CheckForChangeAttackState()
	{
		if (IsWithinStrikingDistance && CheckFinishAttackCoolDown())
		{
			Vector2 direction = (PlayerPos.CurrentValue - (Vector2)transform.position).normalized;
			if (!Physics2D.Raycast(transform.position, direction, AttackRange, Obstacles))
			{
				StateMachine.ChangeState(AttackState);
			}
		}
	}

	public bool CheckRaycastAttack()
	{
		Vector2 direction = (PlayerPos.CurrentValue - (Vector2)transform.position).normalized;
		if(!Physics2D.Raycast(transform.position, direction, AttackRange, Obstacles))
		{
			return true;
		}
		return false;
	}

	#region Animation Triggers
	private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }
    #region Trigger Check
    public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }
    #endregion

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootStepSound,
    }
	#endregion

	public void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, AttackRange);

		try // object null when not in play mode
		{
            if (PlayerPos.CurrentValue != null)
            {
                Vector2 direction = (PlayerPos.CurrentValue - (Vector2)transform.position).normalized;
                Gizmos.DrawRay(transform.position, direction * AttackRange);
            }
        }
		catch
		{
			return;
		}
	}
}
