using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich1State
{
	protected Lich1 boss;
	protected Lich1StateMachine stateMachine;

	public Lich1State(Lich1 lich, Lich1StateMachine bossStateMachine)
	{
		boss = lich;
		stateMachine = bossStateMachine;
	}

	public virtual void Enter() { }
	public virtual void Exit() { }
	public virtual void FrameUpdate() { }
	public virtual void PhysicsUpdate() { }
}
