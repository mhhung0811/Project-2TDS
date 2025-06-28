using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich2StateMachine
{
	public Lich2State CurrentState { get; set; }

	public void Initialize(Lich2State startingState)
	{
		CurrentState = startingState;
		CurrentState.Enter();
	}

	public void ChangeState(Lich2State newState)
	{
		CurrentState.Exit();
		CurrentState = newState;
		CurrentState.Enter();
	}
}
