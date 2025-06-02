using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
	private EnemyUseGun enemyUseGun => (EnemyUseGun)base.Enemy;
	public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Enemy.animator.SetBool("isIdle", true);
    }

    public override void Exit() 
    { 
        base.Exit(); 
        Enemy.animator.SetBool("isIdle", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
		if (Enemy.IsWithinStrikingDistance == false || !Enemy.CheckRaycastAttack())
		{
			EnemyStateMachine.ChangeState(enemyUseGun.ChaseState);
		}
		else
		{
			enemyUseGun.CheckForChangeAttackState();
		}
	}

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
