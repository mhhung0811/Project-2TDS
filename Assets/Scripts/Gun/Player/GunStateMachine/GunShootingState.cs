using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootingState : GunState
{
	private float _startTime;
	public GunShootingState(GunBase gun, GunStateMachine gunStateMachine) : base(gun, gunStateMachine)
	{

	}

	public override void Enter()
	{
		base.Enter();
		_startTime = Time.time;
		Gun.animator.SetBool("IsShooting", true);
	}

	public override void Exit() 
	{
		Gun.animator.SetBool("IsShooting", false);
		Debug.Log("Exit Shooting State");
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
		CheckFinishShooting();
	}

	public override void PhysicsUpdate()
	{
		
	}

	public void CheckFinishShooting()
	{
		if (Time.time - _startTime >= (1.0f / Gun.fireRate))
		{
			Gun.StateMachine.ChangeState(Gun.IdleState);
		}
	}
}
