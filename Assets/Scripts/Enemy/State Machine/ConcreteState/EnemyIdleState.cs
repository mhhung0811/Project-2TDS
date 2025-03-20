using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Enemy._animator.SetBool("isIdle", true);
    }

    public override void Exit() 
    { 
        base.Exit(); 
        Enemy._animator.SetBool("isIdle", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
		if (Enemy.IsWithinStrikingDistance == false || !Enemy.CheckRaycastAttack())
		{
            Debug.Log(Enemy.CheckRaycastAttack());
			Debug.Log("Changing to Chase State");
			EnemyStateMachine.ChangeState(Enemy.ChaseState);
		}
		else
		{
			Enemy.CheckForChangeAttackState();
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
