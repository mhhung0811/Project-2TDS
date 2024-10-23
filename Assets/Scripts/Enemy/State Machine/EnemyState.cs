using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy Enemy;
    protected EnemyStateMachine EnemyStateMachine;

    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        Enemy = enemy;
        EnemyStateMachine = enemyStateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }
}
