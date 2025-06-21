using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnutDieState : EnemyState
{
	private Gunnut gunnut => (Gunnut)base.Enemy;

	public GunnutDieState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		gunnut.animator.SetBool("IsDie", true);
		gunnut.StopAllCoroutines();
		gunnut.unit.StopAllCoroutines();
		gunnut.RB.velocity = Vector2.zero;
		gunnut.GetComponent<Collider2D>().enabled = false;
		gunnut.StartCoroutine(DelaySetActive());
	}

	public override void Exit()
	{
		base.Exit();
		gunnut.animator.SetBool("IsDie", false);
		gunnut.GetComponent<Collider2D>().enabled = true;
	}

	public override void FrameUpdate()
	{

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator DelaySetActive()
	{
		yield return new WaitForSeconds(2f);
		Enemy.gameObject.SetActive(false);
	}
}
