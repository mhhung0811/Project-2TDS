using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Enemy _enemy;
    private EnemyStateMachine _enemyStateMachine;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _enemy = enemy;
        _enemyStateMachine = enemyStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _enemy._animator.SetBool("isIdle", true);
    }

    public override void Exit()
    {
        base.Exit();
        _enemy._animator.SetBool("isIdle", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (_enemy.IsWithinStrikingDistance == false)
        {
            _enemyStateMachine.ChangeState(_enemy.ChaseState);
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
