using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    private Vector2 rollDirection;
    public PlayerRollState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.Animator.SetInteger("State", 2);
        rollDirection = Player.MovementInput;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Player.isRolling = true;
        Player.myRb.velocity = rollDirection * Player.MovementSpeed * 1.5f;
    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);


        if (triggerType == Player.AnimationTriggerType.RollEnd)
        {
            PlayerStateMachine.ChangeState(Player.IdleState);
            Player.isRolling = false;
            if(Player.isPressWASD)
            {
                PlayerStateMachine.ChangeState(Player.MoveState);
            }
        }
    }
}