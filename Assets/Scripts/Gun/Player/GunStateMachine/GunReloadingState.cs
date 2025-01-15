using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReloadingState : GunState
{
	public float startTime;
	public GunReloadingState(GunBase gun, GunStateMachine gunStateMachine) : base(gun, gunStateMachine) { }

	public override void Enter()
	{
		base.Enter();
		startTime = Time.time;
		Gun.playerIsReloading.CurrentValue = true;
		Gun.animator.SetBool("IsReloading", true);
		Gun.changeGunStateEvent.Raise(("IsReloading", true));
		// Debug.Log("Start reloading");
	}

	public override void Exit()
	{
		base.Exit();
		startTime = 0;
		// Debug.Log("Exit reloading");
		Gun.playerIsReloading.CurrentValue = false;
		Gun.animator.SetBool("IsReloading", false);
		Gun.changeGunStateEvent.Raise(("IsReloading", false));
	}

	public override void FrameUpdate()
	{
		base.FrameUpdate();
		Gun.playerReloadTime.CurrentValue = Time.time - startTime;
		CheckFinishReload();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	private void CheckFinishReload()
	{
		if (Time.time - startTime >= Gun.reloadTime)
		{
			Gun.SetAmmoWhenReload();
			Gun.StateMachine.ChangeState(Gun.IdleState);
			// Debug.Log("Finish reloading");
		}
	}
}
