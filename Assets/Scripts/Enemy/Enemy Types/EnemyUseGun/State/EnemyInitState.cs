using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyInitState : EnemyState
{
	private float initTimer;
	private EnemyUseGun enemyUseGun => (EnemyUseGun)base.Enemy;
	public EnemyInitState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{
	}

	public override void Enter()
	{
		initTimer = 0;
		Enemy.animator.SetBool("isInit", true);
		EffectManager.Instance.PlayEffect(EffectType.SpawnEnemy, Enemy.transform.position, Quaternion.identity);
	}

	public override void Exit()
	{
		initTimer = 0;
		Enemy.animator.SetBool("isInit", false);
	}

	public override void FrameUpdate()
	{
		CheckForChangeState();
	}

	public override void PhysicsUpdate()
	{
		
	}

	public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
	{
		
	}

	private void CheckForChangeState()
	{
		initTimer += Time.deltaTime;
		if (initTimer >= Enemy.InitTime)
		{
			EnemyStateMachine.ChangeState(enemyUseGun.IdleState);
		}
	}
}
