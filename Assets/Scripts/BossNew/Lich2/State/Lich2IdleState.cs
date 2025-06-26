using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2IdleState : Lich2State
{
	public bool isCombo = false;
	public Lich2IdleState(Lich2 highPriest, Lich2StateMachine stateMachine) : base(highPriest, stateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Idle", true);
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Idle", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();

		if (Input.GetKeyDown(KeyCode.H))
		{
			stateMachine.ChangeState(boss.attackLeftState);
		}
		else if (Input.GetKeyDown(KeyCode.J))
		{
			stateMachine.ChangeState(boss.attackRightState);
		}
		else if (Input.GetKeyDown(KeyCode.K)) 
		{
			stateMachine.ChangeState(boss.attackCenterState);
		}
		else if (Input.GetKeyDown(KeyCode.L))
		{
			stateMachine.ChangeState(boss.attackState);
		}
		else if (Input.GetKeyDown(KeyCode.U))
		{
			isCombo = true;
			stateMachine.ChangeState(boss.attackLeftState);
		}
	}	

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator AutoChangeState()
	{
		yield return new WaitForSeconds(2f);
		boss.stateMachine.ChangeState(boss.idleState);
	}
}

