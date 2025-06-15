using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestDissolveState : HighPriestState
{
	private float timeToDissolve = 0.3f;
	private float delayAttack = 0.5f;
	public HighPriestDissolveState(HighPriest highPriest, HighPriestStateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.StartCoroutine(Skill());
		boss.col.enabled = false;
		//boss.StartCoroutine(LogicAnimation());
	}

	public override void Exit()
	{
		base.Exit();
		boss.col.enabled = true;
		boss.animator.SetBool("Dissolve", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator Skill()
	{
		foreach(var pos in boss.posTele)
		{
			boss.StartDissolve(timeToDissolve);
			yield return new WaitForSeconds(timeToDissolve);
			boss.transform.position = pos.CurrentValue;
			boss.StartAppear(timeToDissolve);
			boss.animator.SetBool("Idle", false);
			boss.animator.SetBool("Dissolve", true);
			yield return new WaitForSeconds(timeToDissolve);
			boss.animator.SetBool("Dissolve", false);
			boss.animator.SetBool("Idle", true);
		}

		yield return new WaitForSeconds(delayAttack);
		stateMachine.ChangeState(boss.idleState);
	}

	private IEnumerator LogicAnimation()
	{
		yield return new WaitForSeconds(timeToDissolve);
		boss.animator.SetBool("Dissolve", true);
		yield return new WaitForSeconds(timeToDissolve*15f);
		boss.animator.SetBool("Dissolve", false);
		boss.animator.SetBool("Idle", true);
	}
}

