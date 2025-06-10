using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestState
{
	protected HighPriest boss;
	protected HighPriestStateMachine stateMachine;

	public HighPriestState(HighPriest wallMonger, HighPriestStateMachine bossStateMachine)
	{
		boss = wallMonger;
		stateMachine = bossStateMachine;
	}

	public virtual void Enter() { }
	public virtual void Exit() { }
	public virtual void FrameUpdate() { }
	public virtual void PhysicsUpdate() { }
}
