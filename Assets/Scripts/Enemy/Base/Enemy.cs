using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMove, ITriggerCheckable
{
    [field : SerializeField] public int MaxHealth { get; set; } = 6;
	[field: SerializeField] public IntVariable CurrentHealth { get; set; }
    public Rigidbody2D RB { get; set; }
    public Animator _animator;
    public bool IsFacingRight { get; set; } = true;
	[field: SerializeField] public bool IsWithinStrikingDistance { get; set; }
    [field: SerializeField] public float AttackRange { get; set; } = 5f;
	[field: SerializeField] public float MoveSpeed { get; set; }
	[field: SerializeField] public LayerMask Obstacles { get; set; }
	[field: SerializeField] public Vector2Variable PlayerPos { get; set; }


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
        CurrentHealth.SetValue(MaxHealth);
		RB = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		StateMachine.Initialize(ChaseState);
    }

    private void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
        CheckForFlip();
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
		if (IsWithinStrikingDistance)
		{
			Vector2 direction = (PlayerPos.CurrentValue - (Vector2)transform.position).normalized;
			if (!Physics2D.Raycast(transform.position, direction, AttackRange, Obstacles))
			{
				StateMachine.ChangeState(AttackState);
			}
		}
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
