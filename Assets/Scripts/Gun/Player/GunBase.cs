using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{


    public GunStateMachine StateMachine;
    public GunIdleState IdleState;
	public GunShootingState ShootState;
	public GunReloadingState ReloadState;

	private void Awake()
	{
		StateMachine = new GunStateMachine();
		IdleState = new GunIdleState(this, StateMachine);
		ShootState = new GunShootingState(this, StateMachine);
		ReloadState = new GunReloadingState(this, StateMachine);
	}

	void Start()
    {
        StateMachine.Initialize(IdleState);
	}

    void Update()
    {
		StateMachine.CurrentState.FrameUpdate();
	}

	private void FixedUpdate()
	{
		StateMachine.CurrentState.PhysicsUpdate();
	}
}
