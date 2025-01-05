using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{



	#region State Machine Variables
	public BossStateMachine StateMachine { get; set; }
	public BossIdleState IdleState { get; set; }
	public BossMoveState MoveState { get; set; }
	public BossRollState RollState { get; set; }
	#endregion
	private void Awake()
	{
		
	}
	private void Start()
	{
		
	}

	private void Update()
	{
		StateMachine.CurrentState.FrameUpdate();
	}

	private void FixedUpdate()
	{
		StateMachine.CurrentState.PhysicsUpdate();
	}

	#region Animation Triggers
	private void AnimationTriggerEvent(AnimationTriggerType triggerType)
	{
		StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
	}
	public enum AnimationTriggerType
	{
		EnemyDamaged,
		PlayFootStepSound,
	}
	#endregion
}
