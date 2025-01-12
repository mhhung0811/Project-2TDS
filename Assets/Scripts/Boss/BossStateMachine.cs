using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine
{
    public BossState CurrentState { get; set; }

	public void Initialize(BossState startingState)
	{
		CurrentState = startingState;
		CurrentState.Enter();
	}

	public void ChangeState(BossState newState)
	{
		CurrentState.Exit();
		CurrentState = newState;
		CurrentState.Enter();
	}
}
