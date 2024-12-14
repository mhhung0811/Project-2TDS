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
        _unit.speed = _enemy.MoveSpeed; 
        _unit.StartFindPath();
    }

    public override void Exit()
    {
        base.Exit();
        _unit.speed = 0;
		_unit.StopFindPath();
	}

    public override void FrameUpdate()
    {
        base.FrameUpdate();
		CheckForChangeAttackState();
	}

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
		base.AnimationTriggerEvent(triggerType);
	}

    private void CheckForChangeAttackState()
	{
        if (_enemy.IsWithinStrikingDistance)
        {
            Vector2 direction = (_unit.target.transform.position - _enemy.transform.position).normalized;
            if (!Physics2D.Raycast(_enemy.transform.position, direction, _enemy.AttackRange, _enemy.Obstacles))
            {
                _enemy.StateMachine.ChangeState(_enemy.AttackState);
                Debug.Log("Chase -> Attack");
			}
        }
	}
}
