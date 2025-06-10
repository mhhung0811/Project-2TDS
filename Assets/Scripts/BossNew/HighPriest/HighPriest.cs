using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPriest : MonoBehaviour, IEnemyInteractable
{
	#region Boss Properties
	public FloatVariable maxHealth;
	public FloatVariable currentHealth;
	public float moveSpeed;
	#endregion

	#region Interface variables
	public bool IsEnemyInteractable { get; set; } = true;
	#endregion

	#region Boss Components
	[HideInInspector]
	public Animator animator;
	public Animator gunAnimator;
	[HideInInspector]
	public Rigidbody2D rb;
	[HideInInspector]
	public Collider2D col;
	public Material damageFlashMAT;
	#endregion

	#region State Machine
	public HighPriestStateMachine stateMachine;
	public HighPriestInitState initState;
	public HighPriestIdleState idleState;
	public HighPriestAttackState attackState;
	public HighPriestDieState dieState;
	public HighPriestGunState gunState;
	public HighPriestShieldState shieldState;
	public HighPriestDissolveState dissolveState;
	#endregion
	#region Unity Functions 
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		col = GetComponent<Collider2D>();

		stateMachine = new HighPriestStateMachine();
		initState = new HighPriestInitState(this, stateMachine);
		idleState = new HighPriestIdleState(this, stateMachine);
		attackState = new HighPriestAttackState(this, stateMachine);
		dieState = new HighPriestDieState(this, stateMachine);
		gunState = new HighPriestGunState(this, stateMachine);
		shieldState = new HighPriestShieldState(this, stateMachine);
		dissolveState = new HighPriestDissolveState(this, stateMachine);

		stateMachine.Initialize(initState);
	}
	void Start()
    {
        
    }

    void Update()
    {
        
    }

	private void FixedUpdate()
	{
		
	}
	#endregion

	public void OnEnemyBulletHit(float damge)
	{
		if (stateMachine.CurrentState == initState || stateMachine.CurrentState == dieState || !IsEnemyInteractable) return;

		currentHealth.CurrentValue = currentHealth.CurrentValue - damge;
		StartCoroutine(FlashWhite());

		if (currentHealth.CurrentValue <= 0)
		{
			stateMachine.ChangeState(dieState);
		}
	}

	public void PlayFlashWhite()
	{
		StartCoroutine(FlashWhite());
	}
	private IEnumerator FlashWhite()
	{
		damageFlashMAT.SetFloat("_FlashAmount", 1f);
		yield return new WaitForSeconds(0.05f);
		damageFlashMAT.SetFloat("_FlashAmount", 0f);
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

	[ContextMenu("Attack State")]
	public void DebugAttackState()
	{
		Debug.Log("Chuyển sang trạng thái Idle");
		stateMachine.ChangeState(attackState);
	}

	[ContextMenu("Shield State")]
	public void DebugMoveState()
	{
		Debug.Log("Chuyển sang trạng thái Shield");
		stateMachine.ChangeState(shieldState);
	}

	[ContextMenu("Gun State")]
	public void DebugJumpState()
	{
		Debug.Log("Chuyển sang trạng thái Gun");
		stateMachine.ChangeState(gunState);
	}


	[ContextMenu("Die State")]
	public void DebugDieState()
	{
		Debug.Log("Chuyển sang trạng thái Die");
		stateMachine.ChangeState(dieState);
	}

	[ContextMenu("Dissolve State")]
	public void DebugDissolveState()
	{
		Debug.Log("Chuyển sang trạng thái Dissolve");
		stateMachine.ChangeState(dissolveState);
	}
	#endregion

}
