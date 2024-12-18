using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState
{
	protected GunBase Gun;
	protected GunStateMachine GunStateMachine;

	public GunState(GunBase gun, GunStateMachine gunStateMachine)
	{
		Gun = gun;
		GunStateMachine = gunStateMachine;
	}

	public virtual void Enter() { }
	public virtual void Exit() { }
	public virtual void FrameUpdate() { }
	public virtual void PhysicsUpdate() { }
}
