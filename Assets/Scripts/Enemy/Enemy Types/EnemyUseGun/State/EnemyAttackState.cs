using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
	private float _attackTimer;
    private EnemyUseGun enemyUseGun => (EnemyUseGun)base.Enemy;
	public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
	}

    public override void Enter()
    {
        base.Enter();
        Enemy.animator.SetBool("isAttacking", true);
		_attackTimer = Time.time;

		enemyUseGun.Attack();
	}

	public override void Exit()
    {
        base.Exit();
        Enemy.animator.SetBool("isAttacking", false);
		enemyUseGun.attackCooldownTimer = 0;
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
		if (Time.time - _attackTimer >= enemyUseGun.AttackDuration)
		{
			EnemyStateMachine.ChangeState(enemyUseGun.IdleState);
		}
	}
}
