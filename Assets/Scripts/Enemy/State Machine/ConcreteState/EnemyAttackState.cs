using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
	private float _attackTimer;
	public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Enemy._animator.SetBool("isAttacking", true);
		_attackTimer = Time.time;

		Enemy.Attack();
	}

	public override void Exit()
    {
        base.Exit();
        Enemy._animator.SetBool("isAttacking", false);
		Enemy.attackCooldownTimer = 0;
    }

    public override void FrameUpdate()
    {
        UpdateAttackTimer();
	}

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public void UpdateAttackTimer()
	{
		if (Time.time - _attackTimer >= Enemy.AttackDuration)
		{
			EnemyStateMachine.ChangeState(Enemy.IdleState);
		}
	}
}
