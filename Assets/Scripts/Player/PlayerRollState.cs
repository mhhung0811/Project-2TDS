using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    private Vector2 _rollDirection;
    private float _rollDuration;

    public PlayerRollState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Roll");
        base.Enter();
        Player.Animator.SetInteger("State", 2);
        Player.IsRolling = true;
        _rollDuration = Player.RollDuration;
        _rollDirection = Player.MovementInput;

    }

    public override void Exit()
    {
        base.Exit();
        Player.IsRolling = false;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        _rollDuration -= Time.deltaTime;
        if (_rollDuration <= 0)
        {
            ChangeState();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Player.myRb.velocity = _rollDirection * Player.RollSpeed * 1.5f;
    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    private void ChangeState()
    {
        if (Player.IsPressWASD)
        {
            PlayerStateMachine.ChangeState(Player.MoveState);
        }
        else
        {
            PlayerStateMachine.ChangeState(Player.IdleState);
        }
    }
}