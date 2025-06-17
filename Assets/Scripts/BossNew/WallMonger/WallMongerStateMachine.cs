using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerStateMachine 
{
	public WallMongerState CurrentState { get; set; }

	public void Initialize(WallMongerState startingState)
	{
		CurrentState = startingState;
		CurrentState.Enter();
	}

	public void ChangeState(WallMongerState newState)
	{
		CurrentState.Exit();
		CurrentState = newState;
		CurrentState.Enter();
	}
}
