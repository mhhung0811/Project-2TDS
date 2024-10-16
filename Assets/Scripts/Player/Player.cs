using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Animator Animator;
    public Vector2 MovementInput;
    public float MovementSpeed = 5f;
    public Rigidbody2D myRb;
    public bool isFacingRight = true;
    public bool isRolling = false;
    public bool isPressWASD = false;
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
        MovementInput = Vector2.zero;
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

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPressWASD = true;
            MovementInput = context.ReadValue<Vector2>();

            if(isRolling)
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
            isPressWASD = false;
            if (isRolling)
            {
                return;
            }
            StateMachine.ChangeState(IdleState);
        }
    }

    public void Move()
    {
        myRb.velocity = MovementInput * MovementSpeed;
        if (MovementInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (MovementInput.x < 0 && isFacingRight)
        {
            Flip();
        }
    }    

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        Debug.Log("OnRoll");
        if (context.performed)
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
