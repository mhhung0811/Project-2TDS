using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private Vector2 _movementInput;
    private Coroutine _soundFootCoroutine;
	public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.Animator.SetBool("IsMoving", true);
		if(_soundFootCoroutine == null)
		{
			_soundFootCoroutine = Player.StartCoroutine(SoundFoot());
		}
	}

    public override void Exit()
    {
        base.Exit();
        if(_soundFootCoroutine != null)
		{
			Player.StopCoroutine(_soundFootCoroutine);
			_soundFootCoroutine = null;
		}
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

    public IEnumerator SoundFoot()
    {
        while (true)
        {
            SoundManager.Instance.PlaySound("FootStep", 0.2f);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
