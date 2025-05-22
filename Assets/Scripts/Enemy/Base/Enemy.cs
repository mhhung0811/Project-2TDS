using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemyMove, ITriggerCheckable, IEnemyInteractable, IExplodedInteractable, IDamageEffectApplicable
{
	// Base Enemy Variables
	[field : SerializeField] public int MaxHealth { get; set; }
	[field: SerializeField] public int CurrentHealth { get; set; }
	[field: SerializeField] public LayerMask Obstacles { get; set; }
	[field: SerializeField] public Vector2Variable PlayerPos { get; set; }
	[field: SerializeField] public float InitTime { get; set; }
	[field: SerializeField] public float DieTime { get; set; }

	[field: SerializeField] protected EnemyTypeEvent onEnemyDown;

	[HideInInspector]
	public Animator animator;


	// implement interface ITriggerCheckable 
	[HideInInspector] public bool IsWithinStrikingDistance { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }


	// implement interface IEnemyMove
	[field: SerializeField] public float MoveSpeed { get; set; }
    public bool IsFacingRight { get; set; } = true;
	public Rigidbody2D RB { get; set; }


	// implement interface IEnemyInteractable
	public bool IsEnemyInteractable { get; set; }


	// implement interface IExplodedInteractable
	public bool CanExplodeInteractable { get; set; } = true;

	public EnemyStateMachine StateMachine { get; set; }

	/// <summary>
	/// Function -----------------------------------------------
	/// </summary>

	#region Functions Base
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
	

	public bool CheckRaycastAttack()
	{
		Vector2 distance = PlayerPos.CurrentValue - (Vector2)transform.position;
		Vector2 direction = distance.normalized;

		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, AttackRange, Obstacles);

		if (hit.collider != null)
		{
			float distanceToObstacle = hit.distance;
			if (distanceToObstacle < distance.magnitude)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		return true;
	}
	#endregion



	// implement interface IEnemyInteractable
	public virtual void OnEnemyBulletHit(float damage)
	{
		
	}


	// implement interface IExplodedInteractable
	public virtual void OnExplode(float damage)
	{

	}


	// implement interface IEnemyMove
	#region IEnemyMovable
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
	#endregion


	// implement interface IDamageEffectApplicable
	public void Accept(IDamageEffectVisitor visitor)
	{
		visitor.Visit(this);
	}


	// implement interface ITriggerCheckable
	public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }


	#region Animation Triggers
	private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }
	#region Trigger Check

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
