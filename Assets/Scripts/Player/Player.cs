using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public IntVariable HP;
	public IntVariable MaxHP;
    public Vector2Variable PlayerPos;

	public Camera MainCamera;
    public Animator Animator;
    public Rigidbody2D myRb;
    public Vector2 MovementInput;

    public float MovementSpeed = 5f;
    public float RollSpeed = 6f;
    public float RollDuration = 0.4f;

    public bool IsFacingRight = true;
    public bool IsRolling = false;
    public bool IsPressWASD = false;
    public bool IsPressShoot = false;

	public AWM awm;
    public ShotGun shotGun;

	public FactorySpawnEvent factorySpawnEvent;
	public VoidIntVector2FloatFuncProvider spawnGunPrefFunc;

	[Header("Interaction Zone")]
	public float interactionOffSet = 0.25f;
	public CircleCollider2D interactCollider;

    #region State Machine Variables
    public PlayerStateMachine StateMachine;
    public PlayerIdleState IdleState;
    public PlayerMoveState MoveState;
    public PlayerRollState RollState;
    #endregion
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        RollState = new PlayerRollState(this, StateMachine);
        IdleState = new PlayerIdleState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
    }
    void Start()
    {
		awm = GetComponentInChildren<AWM>();
		shotGun = GetComponentInChildren<ShotGun>();
		IsFacingRight = true;
		MovementInput = new Vector2(1, 0);
        StateMachine.Initialize(IdleState);
        
        spawnGunPrefFunc.GetFunction()?.Invoke((0, Vector2.zero, 0));
    }

    void Update()
    {
        PlayerPos.SetValue(transform.position);
		StateMachine.CurrentState.FrameUpdate();
		UpdateInteractColliderByPosMouse();
        OnShoot();
	}
    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void InputShoot(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            IsPressShoot = true;
		}
		else if (context.canceled)
		{
			IsPressShoot = false;
		}

	}
    public void OnShoot()
    {
        if (!IsPressShoot) return;
        if(IsRolling) return;

		Vector2 mousePosition = Mouse.current.position.ReadValue();

		Vector2 worldPosition = MainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, MainCamera.nearClipPlane));

		float angle = Vector2ToAngle(worldPosition - new Vector2(transform.position.x, transform.position.y));

		// awm.Shoot(angle);
		shotGun.Shoot(angle);
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

    public void InputMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsPressWASD = true;
            MovementInput = context.ReadValue<Vector2>();

            if(IsRolling)
            {
                return;
            }

            if (MovementInput != Vector2.zero)
            {
                StateMachine.ChangeState(MoveState); 
            }
        }
        else if (context.canceled)
        {
            IsPressWASD = false;
            if (IsRolling)
            {
                return;
            }
            StateMachine.ChangeState(IdleState);
        }
    }

    public void Move()
    {
        myRb.velocity = MovementInput * MovementSpeed;
        //if (MovementInput.x > 0 && !IsFacingRight)
        //{
        //    Flip();
        //}
        //else if (MovementInput.x < 0 && IsFacingRight)
        //{
        //    Flip();
        //}
    }

    public void Flip()
    {
        IsFacingRight = !IsFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void InputRoll(InputAction.CallbackContext context)
    {
        if (context.performed && !IsRolling && IsPressWASD)
        {
            StateMachine.ChangeState(RollState);
        }
    }
    
    public void InputInteract(InputAction.CallbackContext context)
    {
	    if (context.performed)
	    {
		    Interact();
	    }
    }

    public void Interact()
    {
	    interactCollider.GetComponent<InteractionZone>().Interact(this.gameObject);
    }

	// Get position of mouse
	public Vector2 GetMousePosition()
	{
		Vector2 mousePosition = Mouse.current.position.ReadValue();
		Vector2 worldPosition = MainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, MainCamera.nearClipPlane));
		return worldPosition;
	}

    public void UpdateAnimationByPosMouse()
    {
        Vector2 mousePosition = GetMousePosition();
        Vector2 direction = mousePosition - new Vector2(transform.position.x, transform.position.y);
        Animator.SetFloat("XInput", direction.x);
		Animator.SetFloat("YInput", direction.y);
		// Check Flip
		if (direction.x > 0 && !IsFacingRight)
		{
			Flip();
		}
		else if (direction.x < 0 && IsFacingRight)
		{
			Flip();
		}
	}
    
    public void UpdateInteractColliderByPosMouse()
    {
		// Get the mouse position in world coordinates
		Vector2 mousePosition = GetMousePosition();

		// Calculate the angle between the player and the mouse
		Vector2 direction = mousePosition - (Vector2)transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x); // Angle in radians

		Vector2 newColliderPosition = new Vector2(
			transform.position.x + Mathf.Cos(angle) * interactionOffSet,
			transform.position.y + Mathf.Sin(angle) * interactionOffSet
		);
		// Calculate the new position of the collider using trigonometry

		// Update the collider's position
		interactCollider.transform.position = newColliderPosition;
	}

	#region Animation Triggers
	public void AnimationTriggerEvent(AnimationTriggerType triggerEvent)
    {
        StateMachine.CurrentState.AnimationTriggerEvent(triggerEvent);
    }
    public enum AnimationTriggerType
    {
        RollEnd,
    }
    #endregion
}
