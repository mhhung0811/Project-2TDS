using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerState
{
	public PlayerDieState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		Player.Animator.SetBool("IsDie", true);
		Player.myRb.velocity = Vector2.zero;
		Player.myRb.constraints = RigidbodyConstraints2D.FreezeAll;
		Player.StartCoroutine(Die());
	}

	public override void Exit()
	{
		base.Exit();
		Player.Animator.SetBool("IsDie", false);
	}

	public override void FrameUpdate()
	{
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
	{
		base.AnimationTriggerEvent(triggerType);

	}

	public IEnumerator Die()
	{
		yield return new WaitForSeconds(1.5f);
		Void @void = new Void();
		Player.PlayerDied.Raise(@void);
	}
} 
