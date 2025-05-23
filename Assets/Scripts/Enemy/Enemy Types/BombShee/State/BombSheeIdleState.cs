using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSheeIdleState : EnemyState
{
    private BombShee bombShee => (BombShee)base.Enemy;

	public BombSheeIdleState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
	{

	}

	public override void Enter()
	{
		
	}

	public override void Exit()
	{

	}

	public override void FrameUpdate()
	{
		if (!bombShee.OnProtectRange())
		{
			EnemyStateMachine.ChangeState(bombShee.MoveState);
		}
		bombShee.Screech();
	}

	public override void PhysicsUpdate()
	{

	}

	public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
	{

	}

	private IEnumerator TestState()
	{
		yield return new WaitForSeconds(2f);
		//bombShee.Screech();
	}
}
