using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTailWhipState : BossState
{
	public BossTailWhipState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		Boss.Animator.SetBool("IsTailWhip", true);
		Boss.StartCoroutine(ChangeIdle());
		Boss.StartCoroutine(Attack());
	}

	public override void Exit()
	{
		base.Exit();
		Boss.Animator.SetBool("IsTailWhip", false);
		Boss.StartCoroutine(Boss.TailWhipSkill());
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void AnimationTriggerEvent(Boss.AnimationTriggerType triggerType)
	{

	}

	public IEnumerator Attack()
	{
		float radius = 12;
		float startAngle = 30;
		float endAngle = 90;
		float quantity = 36;
		float angleStep = (endAngle - startAngle) / quantity;
		for (int i = 0; i < quantity ; i++)
		{
			float angle = endAngle - i * angleStep;
			float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
			float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
			Vector2 offset = new Vector2(x, y) + new Vector2(0, -radius);
			Boss.TakeBulletEvent.Raise((FlyweightType.TailWhipBullet, Boss.BossPos.CurrentValue + offset, 0));
		}
		yield return null;
	}

	private IEnumerator ChangeIdle()
	{
		yield return new WaitForSeconds(2f);
		Boss.StateMachine.ChangeState(Boss.IdleState);
	}
}
