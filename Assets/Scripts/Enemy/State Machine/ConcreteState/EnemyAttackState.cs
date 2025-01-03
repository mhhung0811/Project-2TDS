using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
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
        if (Enemy.IsWithinStrikingDistance == false)
        {
            EnemyStateMachine.ChangeState(Enemy.ChaseState);
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
