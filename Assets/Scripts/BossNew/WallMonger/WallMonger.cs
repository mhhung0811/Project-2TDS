using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMonger : MonoBehaviour
{
	#region Boss Properties
	#endregion

	#region Boss Components
	public Animator animator;
	public Rigidbody2D rb;
	#endregion

	#region State Machine
	public WallMongerStateMachine stateMachine;
	public WallMongerInitState initState;
	public WallMongerIdleState idleState;
	public WallMongerMoveState moveState;
	public WallMongerJumpState jumpState;
	public WallMongerSkillState skillState;
	#endregion

	private void Awake()
	{
		// Get Components
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();

		// Initialize State Machine
		stateMachine = new WallMongerStateMachine();
		initState = new WallMongerInitState(this, stateMachine);
		idleState = new WallMongerIdleState(this, stateMachine);
		moveState = new WallMongerMoveState(this, stateMachine);
		jumpState = new WallMongerJumpState(this, stateMachine);
		skillState = new WallMongerSkillState(this, stateMachine);
		stateMachine.Initialize(initState);
	}


	private void Start()
	{

	}

	private void Update()
	{
		stateMachine.CurrentState.FrameUpdate();
	}

	private void FixedUpdate()
	{
		stateMachine.CurrentState.PhysicsUpdate();
	}


	#region Unity Methods Debug
	[ContextMenu("Init State")]
	public void DebugInitState()
	{
		Debug.Log("Chuyển sang trạng thái Init");
		stateMachine.ChangeState(initState);
	}

	[ContextMenu("Idle State")]
	public void DebugIdleState()
	{
		Debug.Log("Chuyển sang trạng thái Idle");
		stateMachine.ChangeState(idleState);
	}

	[ContextMenu("Move State")]
	public void DebugMoveState()
	{
		Debug.Log("Chuyển sang trạng thái Move");
		stateMachine.ChangeState(moveState);
	}

	[ContextMenu("Jump State")]
	public void DebugJumpState()
	{
		Debug.Log("Chuyển sang trạng thái Jump");
		stateMachine.ChangeState(jumpState);
	}

	[ContextMenu("Skill State")]
	public void DebugSkillState()
	{
		Debug.Log("Chuyển sang trạng thái Skill");
		stateMachine.ChangeState(skillState);
	}
	#endregion

	private void OnDrawGizmos()
	{
		
	}
}
