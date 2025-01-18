using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerRollState : PlayerState
{
    private Vector2 _rollDirection;
    private float _rollDuration;

    public PlayerRollState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        // Debug.Log("Roll");
        base.Enter();
        Player.Animator.SetBool("IsRolling", true);
        Player.Animator.SetFloat("XInput", Player.MovementInput.x);
		Player.Animator.SetFloat("YInput", Player.MovementInput.y);

		Player.IsRolling = true;
        _rollDuration = Player.RollDuration;
        _rollDirection = Player.MovementInput;

		// Check Flip
		if (Player.MovementInput.x > 0 && !Player.IsFacingRight)
		{
			Player.Flip();
		}
		else if (Player.MovementInput.x < 0 && Player.IsFacingRight)
		{
			Player.Flip();
		}

        Player.HoldGun.SetActive(false);

        // bat tu khi roll
        Player.isInvulnerable = true;
		Player.IsPlayerInteractable = false;
		Player.myRb.velocity = _rollDirection * Player.RollSpeed;

        SoundManager.Instance.PlaySound("PlayerDodgeLeap");
        Player.StartCoroutine(AnimationRollLand());
	}

    public override void Exit()
    {
        base.Exit();
        Player.IsRolling = false;
		Player.Animator.SetBool("IsRolling", false);
		Player.HoldGun.SetActive(true);
        Player.myRb.velocity = Vector2.zero;

		// het bat tu khi roll
		Player.isInvulnerable = false;
        Player.IsPlayerInteractable = true;
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

	public IEnumerator AnimationRollLand()
	{
		yield return new WaitForSeconds(0.5f);
		SoundManager.Instance.PlaySound("PlayerDodgeRoll");
		// Debug.Log("Roll Land");
		EffectManager.Instance.PlayEffect(EffectType.EfRollLand, Player.transform.position + new Vector3(0, -0.2f, 0), Quaternion.identity);
		Player.myRb.velocity = Player.MovementInput.normalized * Player.RollSpeed / 2;
	}
}