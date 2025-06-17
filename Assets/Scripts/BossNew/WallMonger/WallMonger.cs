using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallMonger : MonoBehaviour, IEnemyInteractable
{
	#region Boss Properties
	public FloatVariable maxHealth;
	public FloatVariable currentHealth;
	public float moveSpeed;

	[HideInInspector]
	public bool canUseSkill = true;
	public float timeToUseSkill = 10f;
	private float cooldownSkill = 10f;
	#endregion

	#region Interface variables
	public bool IsEnemyInteractable { get; set; } = true;
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
	public GameObjectFlyweightTypeVector2FloatFuncProvider takeBulletFunc;
	public Material damageFlashMAT;
	public List<Transform> listSpawnPos;
	public GameObject cameraBoss;
	public GameObject cameraBossInit;
	public GameObject colliderRangeMovePlayer;

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

		// Set up Skill
		cooldownSkill = timeToUseSkill;
		canUseSkill = false;
	}

	private void Update()
	{
		stateMachine.CurrentState.FrameUpdate();

		// Update cooldown for skill usage
		UpdateCoolDownSkill();
	}

	private void FixedUpdate()
	{
		stateMachine.CurrentState.PhysicsUpdate();
	}

	public float Vector2ToAngle(Vector2 direction)
	{
		float angleInRadians = Mathf.Atan2(direction.y, direction.x);

		float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

		if (angleInDegrees < 0)
		{
			angleInDegrees += 360f;
		}

		return angleInDegrees;
	}

	public void OnEnemyBulletHit(float damge)
	{
		if(stateMachine.CurrentState == initState || stateMachine.CurrentState == dieState || !IsEnemyInteractable) return;

		currentHealth.CurrentValue = currentHealth.CurrentValue - damge;
		StartCoroutine(FlashWhite());

		if (currentHealth.CurrentValue <= 0)
		{
			stateMachine.ChangeState(dieState);
		}
	}

	public void UseSkill()
	{
		// reset cooldown
		cooldownSkill = timeToUseSkill;
		canUseSkill = false;

		stateMachine.ChangeState(skillState);
	}

	private void UpdateCoolDownSkill()
	{
		if(cooldownSkill <= 0f) return;

		cooldownSkill -= Time.deltaTime;

		if(cooldownSkill <= 0)
		{
			cooldownSkill = 0f;
			canUseSkill = true;
		}
	}

	#region Spawn Bullets
	public void SpawnABulletLine(Vector2 posStart, int bulletCount, float width)
	{
		float angle = Vector2ToAngle(Vector2.down);
		float offsetX = width / (bulletCount - 1);
		for (int i = 0; i < bulletCount; i++)
		{
			Vector2 posSpawn = posStart + Vector2.right * (i * offsetX);
			takeBulletFunc.GetFunction()((FlyweightType.EnemyBullet, posSpawn, angle));

			EffectManager.Instance.PlayEffect(EffectType.EfBulletCollide, posSpawn, Quaternion.identity);
		}
	}

	public void SpawnArcBullets(Vector2 pos, Vector2 direction, float totalAngle, int bulletCount)
	{
		float startAngle = -totalAngle / 2f;
		float stepAngle = totalAngle / (bulletCount - 1);

		for (int i = 0; i < bulletCount; i++)
		{
			float angle = startAngle + i * stepAngle;

			takeBulletFunc.GetFunction()((
				FlyweightType.EnemyBullet,
				pos,
				Vector2ToAngle(direction) + angle
			));
		}
	}
	#endregion

	public void PlayFlashWhite()
	{
		StartCoroutine(FlashWhite());
	}

	private IEnumerator FlashWhite()
	{
		damageFlashMAT.SetFloat("_FlashAmount", 0.5f);
		yield return new WaitForSeconds(0.1f);
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
		foreach (Transform pos in listSpawnPos)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(pos.position, 0.1f);
		}
	}
}
