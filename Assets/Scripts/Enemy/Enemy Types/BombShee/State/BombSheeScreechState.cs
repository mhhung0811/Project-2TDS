using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSheeScreechState : EnemyState
{
	private BombShee bombShee => (BombShee)base.Enemy;

	public BombSheeScreechState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
	{

	}

	public override void Enter()
	{
		bombShee.animator.SetBool("IsScreech", true);
		bombShee.SkillBombShee.StartScreech();
		bombShee.StartCoroutine(Screeching());
	}

	public override void Exit()
	{
		bombShee.animator.SetBool("IsScreech", false);
	}

	public override void FrameUpdate()
	{

	}

	public override void PhysicsUpdate()
	{

	}

	public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
	{

	}

	private IEnumerator Screeching()
	{
		yield return new WaitForSeconds(bombShee.timeScreech);
		bombShee.SkillBombShee.EndScreech();
		yield return new WaitForSeconds(bombShee.cooldownScreech);
		bombShee.canScreech = true;
	}
}
