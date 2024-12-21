using UnityEngine;

public class EnemyChaseState : EnemyState
{
	private Unit _unit;

	public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
		_unit = enemy.GetComponent<Unit>();
	}

	public override void Enter()
    {
        base.Enter();
        Enemy._animator.SetBool("isChasing", true);
        _unit.speed = Enemy.MoveSpeed; 
        _unit.StartFindPath();
    }

    public override void Exit()
    {
        base.Exit();
        Enemy._animator.SetBool("isChasing", false);
        _unit.speed = 0;
		_unit.StopFindPath();
	}

    public override void FrameUpdate()
    {
        base.FrameUpdate();
		Enemy.CheckForChangeAttackState();
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
