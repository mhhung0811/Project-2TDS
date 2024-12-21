using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunOutOfAmmoState : GunState
{
	public GunOutOfAmmoState(GunBase gun, GunStateMachine stateMachine) : base(gun, stateMachine){}

	public override void Enter()
	{
		base.Enter();
		Debug.Log("Enter state Out of ammo");
	}

	public override void Exit()
	{
		base.Exit();
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
