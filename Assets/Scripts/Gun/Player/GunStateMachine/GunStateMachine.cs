using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStateMachine
{
	public GunState CurrentState { get; set; }

	public void Initialize(GunState startingState)
	{
		CurrentState = startingState;
		CurrentState.Enter();
	}

	public void ChangeState(GunState newState)
	{
		CurrentState.Exit();
		CurrentState = newState;
		CurrentState.Enter();
	}
}
