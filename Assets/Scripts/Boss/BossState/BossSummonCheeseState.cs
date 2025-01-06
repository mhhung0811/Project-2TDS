using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonCheeseState : BossState
{
	private float timeToSummonCheese = 5f;
	private float timeSpawnCheese = 0.5f;
	public BossSummonCheeseState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
	{
	}

	public override void Enter()
	{
		base.Enter();
		Boss.Animator.SetBool("IsSummonCheese", true);
		Boss.StartCoroutine(Attack());
	}

	public override void Exit()
	{
		base.Exit();
		Boss.Animator.SetBool("IsSummonCheese", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void AnimationTriggerEvent(Boss.AnimationTriggerType triggerType)
	{

	}

	public IEnumerator Attack()
	{
		yield return new WaitForSeconds(timeSpawnCheese);
		GameObject gameObject = GameObject.Instantiate(Boss.Cheese, (Vector2)Boss.transform.position + new Vector2(0, -0.4f), Quaternion.identity);
		yield return new WaitForSeconds(timeToSummonCheese);
		BossStateMachine.ChangeState(Boss.CheeseSlamState);
	}
}
