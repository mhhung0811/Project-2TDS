using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : EnemyState
{
	private float dieTime;
	public EnemyDieState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{
	}

	public override void Enter()
	{
		dieTime = 0;
		Enemy.animator.SetBool("isDie", true);
		// Debug.Log("Enemy Die");
		Enemy.StopAllCoroutines();
		Enemy.StartCoroutine(DelaySetActive());
	}

	public override void Exit()
	{
		dieTime = 0;
		Enemy.animator.SetBool("isDie", false);
		// Debug.Log("Enemy Die Exit");
	}

	public override void FrameUpdate()
	{
	}

	public override void PhysicsUpdate()
	{

	}

	public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
	{

	}

	private IEnumerator DelaySetActive()
	{
		yield return new WaitForSeconds(2f);
		Enemy.gameObject.SetActive(false);
	}
}
