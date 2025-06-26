using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2State
{
	protected Lich2 boss;
	protected Lich2StateMachine stateMachine;

	public Lich2State(Lich2 lich, Lich2StateMachine bossStateMachine)
	{
		boss = lich;
		stateMachine = bossStateMachine;
	}

	public virtual void Enter() { }
	public virtual void Exit() { }
	public virtual void FrameUpdate() { }
	public virtual void PhysicsUpdate() { }
}
