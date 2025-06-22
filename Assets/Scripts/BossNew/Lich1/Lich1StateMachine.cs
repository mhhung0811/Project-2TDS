using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich1StateMachine
{
	public Lich1State CurrentState { get; set; }

	public void Initialize(Lich1State startingState)
	{
		CurrentState = startingState;
		CurrentState.Enter();
	}

	public void ChangeState(Lich1State newState)
	{
		CurrentState.Exit();
		CurrentState = newState;
		CurrentState.Enter();
	}
}
