using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private Vector2 _movementInput;
    public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.Animator.SetBool("IsMoving", true);
	}

    public override void Exit()
    {
        base.Exit();
	}

    public override void FrameUpdate()
    {
        base.FrameUpdate();
		Player.UpdateAnimationByPosMouse();
        _movementInput = Player.MovementInput;
        if (_movementInput == Vector2.zero)
        {
            PlayerStateMachine.ChangeState(Player.IdleState);
        }
    }

	public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
		Player.Move();
	}

	public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
