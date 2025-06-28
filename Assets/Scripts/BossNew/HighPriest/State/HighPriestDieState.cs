using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestDieState : HighPriestState
{
	public HighPriestDieState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.StopAllCoroutines();
		boss.col.enabled = false;
		boss.animator.SetBool("Die", true);
		GameObject shadow = boss.transform.Find("Shadow").gameObject;
		boss.gunAnimator.SetBool("Idle", true);
		if (shadow != null)
		{
			shadow.SetActive(false);
		}
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Die", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}

