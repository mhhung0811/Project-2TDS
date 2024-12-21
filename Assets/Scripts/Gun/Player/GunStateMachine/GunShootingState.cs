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
		Debug.Log("Start shooting");
	}

	public override void Exit() 
	{
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
			Debug.Log("Finish shooting");
		}
	}
}
