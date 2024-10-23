using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Vector3 _targetPos;
    private Vector3 _direction;
    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _targetPos = GetRamdonPointInCircle();
    }

    public override void Exit() 
    { 
        base.Exit(); 
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _direction = (_targetPos - Enemy.transform.position).normalized;
        Debug.Log(_direction);
        Enemy.MoveEnemy(_direction * Enemy.RamdomMovementSpeed);

        if ((Enemy.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            _targetPos = GetRamdonPointInCircle();
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

    private Vector3 GetRamdonPointInCircle()
    {
        return Enemy.transform.position + (Vector3)Random.insideUnitCircle * Enemy.RandomMovementRange;
    }
}
