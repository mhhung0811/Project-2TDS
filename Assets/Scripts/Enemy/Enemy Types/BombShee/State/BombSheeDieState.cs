using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSheeDieState : EnemyState
{
    private BombShee bombShee => (BombShee)base.Enemy;

	public BombSheeDieState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
	{

	}

	public override void Enter()
	{
		Debug.Log("BombShee: Enter DieState");
		bombShee.animator.SetBool("IsDie", true);
		bombShee.StartCoroutine(WaitDie());
		bombShee.SkillBombShee.EndScreech();
	}

	public override void Exit()
	{
		bombShee.animator.SetBool("IsDie", false);
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

	private IEnumerator WaitDie()
	{
		yield return new WaitForSeconds(0.5f);
		bombShee.gameObject.SetActive(false);
	}
}
