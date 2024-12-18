using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public IntVariable HP;
	public IntVariable MaxHP;

	public Camera MainCamera;
    public Animator Animator;
    public Rigidbody2D myRb;
    public Vector2 MovementInput;

    public float MovementSpeed = 5f;
    public float RollSpeed = 6f;
    public float RollDuration = 0.5f;

    public bool IsFacingRight = true;
    public bool IsRolling = false;
    public bool IsPressWASD = false;

    public AWM awm;
    public ShortGun shortGun;

	public FactorySpawnEvent factoryDespawnEvent;

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
		shortGun = GetComponentInChildren<ShortGun>();
		IsFacingRight = true;
		MovementInput = new Vector2(1, 0);
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        StateMachine.CurrentState.FrameUpdate();
    }
    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            Vector2 worldPosition = MainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, MainCamera.nearClipPlane));
            
            float angle = Vector2ToAngle(worldPosition - new Vector2(transform.position.x, transform.position.y));

            //awm.Shoot(angle);
            shortGun.Shoot(angle);
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

    public void OnMove(InputAction.CallbackContext context)
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
        if (MovementInput.x > 0 && !IsFacingRight)
        {
            Flip();
        }
        else if (MovementInput.x < 0 && IsFacingRight)
        {
            Flip();
        }
    }    

    public void Flip()
    {
        IsFacingRight = !IsFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed && !IsRolling && IsPressWASD)
        {
            StateMachine.ChangeState(RollState);
        }
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
