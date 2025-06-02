using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerState
{
	protected WallMonger boss;
	protected WallMongerStateMachine stateMachine;

	public WallMongerState(WallMonger wallMonger, WallMongerStateMachine bossStateMachine)
	{
		boss = wallMonger;
		stateMachine = bossStateMachine;
	}

	public virtual void Enter() { }
	public virtual void Exit() { }
	public virtual void FrameUpdate() { }
	public virtual void PhysicsUpdate() { }
}
