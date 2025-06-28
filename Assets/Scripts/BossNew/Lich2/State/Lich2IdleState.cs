using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2IdleState : Lich2State
{
	public bool isCombo = false;
	private int stack = 1;
	private int stackCombo = 0;
	private float idleDuration;
	public Lich2IdleState(Lich2 highPriest, Lich2StateMachine stateMachine) : base(highPriest, stateMachine)
	{
		stack = 1;
		idleDuration = 3f;
		isCombo = false;
		stackCombo = 0;
	}

	public override void Enter()
	{
		base.Enter();
		boss.animator.SetBool("Idle", true);
		boss.StartCoroutine(ControllChangeState());
	}

	public override void Exit()
	{
		base.Exit();
		boss.animator.SetBool("Idle", false);
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
	}	

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private IEnumerator ControllChangeState()
	{
		yield return new WaitForSeconds(idleDuration);

		switch(stack)
		{
			case 1:
				if(stackCombo == 2)
				{
					isCombo = true;
					stateMachine.ChangeState(boss.attackLeftState);
					stackCombo = 0;
					stack = 3;
					idleDuration = 6f;
				}
				else
				{
					stateMachine.ChangeState(boss.attackLeftState);
					stack++;
					idleDuration = 3f;
				}
				break;
			case 2:
				stateMachine.ChangeState(boss.attackRightState);
				stack++;
				idleDuration = 3f;
				break;
			case 3:
				stateMachine.ChangeState(boss.attackState);
				stack++;
				if(boss.attackState.preSkill1)
				{
					idleDuration = 2f;
				}
				else
				{
					idleDuration = 4f;
				}
				break;
			case 4:
				stateMachine.ChangeState(boss.attackCenterState);
				stack = Random.Range(1, 3);
				stackCombo++;
				if(stackCombo == 2)
				{
					stack = 1;
				}
				idleDuration = 10f;
				break;
			default:
				stateMachine.ChangeState(boss.attackLeftState);
				stack = 1;
				break;
		}
	}
}

