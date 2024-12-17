using UnityEngine;

public class EnemyChaseState : EnemyState
{
	private Unit _unit;
    private Enemy _enemy;

	public EnemyChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
		_unit = enemy.GetComponent<Unit>();
		_enemy = enemy;
	}

	public override void Enter()
    {
        base.Enter();
        _enemy._animator.SetBool("isChasing", true);
        _unit.speed = _enemy.MoveSpeed; 
        _unit.StartFindPath();
    }

    public override void Exit()
    {
        base.Exit();
        _enemy._animator.SetBool("isChasing", false);
        _unit.speed = 0;
		_unit.StopFindPath();
	}

    public override void FrameUpdate()
    {
        base.FrameUpdate();
		_enemy.CheckForChangeAttackState();
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
