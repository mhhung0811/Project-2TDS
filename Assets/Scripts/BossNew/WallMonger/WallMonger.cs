using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMonger : MonoBehaviour
{
	#region Boss Properties
	#endregion

	#region Boss Components
	[HideInInspector]
	public Animator animator;
	[HideInInspector]
	public Rigidbody2D rb;
	[HideInInspector]
	public WallMongerVFX vfx;
	[HideInInspector]
	public Collider2D col;

	public GameObject colliderAlive;
	public GameObject colliderDie;

	[HideInInspector]
	public SpriteRenderer spriteRenderer;
	#endregion

	#region State Machine
	public WallMongerStateMachine stateMachine;
	public WallMongerInitState initState;
	public WallMongerIdleState idleState;
	public WallMongerMoveState moveState;
	public WallMongerJumpState jumpState;
	public WallMongerSkillState skillState;
	public WallMongerDieState dieState;
	#endregion

	private void Awake()
	{
		// Get Components
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		vfx = GetComponent<WallMongerVFX>();
		col = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		// Initialize State Machine
		stateMachine = new WallMongerStateMachine();
		initState = new WallMongerInitState(this, stateMachine);
		idleState = new WallMongerIdleState(this, stateMachine);
		moveState = new WallMongerMoveState(this, stateMachine);
		jumpState = new WallMongerJumpState(this, stateMachine);
		skillState = new WallMongerSkillState(this, stateMachine);
		dieState = new WallMongerDieState(this, stateMachine);
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

	[ContextMenu("Die State")]
	public void DebugDieState()
	{
		Debug.Log("Chuyển sang trạng thái Die");
		stateMachine.ChangeState(dieState);
	}
	#endregion

	private void OnDrawGizmos()
	{
		
	}
}
