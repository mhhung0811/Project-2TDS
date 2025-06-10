using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriestStateMachine
{
	public HighPriestState CurrentState { get; set; }

	public void Initialize(HighPriestState startingState)
	{
		CurrentState = startingState;
		CurrentState.Enter();
	}

	public void ChangeState(HighPriestState newState)
	{
		CurrentState.Exit();
		CurrentState = newState;
		CurrentState.Enter();
	}
}
