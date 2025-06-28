using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Lich2 : MonoBehaviour, IEnemyInteractable
{
	#region Boss Properties
	[Header("Properties")]
	public FloatVariable maxHealth;
	public FloatVariable currentHealth;
	public float moveSpeed;
	#endregion

	#region Interface variables
	public bool IsEnemyInteractable { get; set; } = true;
	#endregion

	#region Boss Components
	[Header("Component")]
	public GameObjectFlyweightTypeVector2FloatFuncProvider takeBulletFunc;
	public Vector2Variable playerPos;

	private SpriteRenderer spriteRenderer;
	private Rigidbody2D rb;
	[HideInInspector]
	public Collider2D col;
	[HideInInspector]
	public Animator animator;

	public Material dissolveMAT;
	#endregion


	#region Child Components
	[Header("Trail fx")]
	public GameObject trailCenter;
	public GameObject trailLeft;
	public GameObject trailRight;

	[Header("Position Transforms")]
	public Transform posCenter;
	public Transform posLeft;
	public Transform posRight;
	public Transform areaLeft;
	public Transform areaRight;
	public Transform areaCenter;
	#endregion

	#region State Machine
	public Lich2StateMachine stateMachine;
	public Lich2InitState initState;
	public Lich2IdleState idleState;
	public Lich2DieState dieState;
	public Lich2AttackLeftState attackLeftState;
	public Lich2AttackRightState attackRightState;
	public Lich2AttackCenterState attackCenterState;
	public Lich2AttackState attackState;
	#endregion

	#region Unity Functions 
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		col = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		//damageFlashMATRunTime = new Material(damageFlashMAT);

		stateMachine = new Lich2StateMachine();
		initState = new Lich2InitState(this, stateMachine);
		idleState = new Lich2IdleState(this, stateMachine);
		dieState = new Lich2DieState(this, stateMachine);
		attackLeftState = new Lich2AttackLeftState(this, stateMachine);
		attackRightState = new Lich2AttackRightState(this, stateMachine);
		attackCenterState = new Lich2AttackCenterState(this, stateMachine);
		attackState = new Lich2AttackState(this, stateMachine);

		stateMachine.Initialize(initState);
	}
	private void Start()
	{
		dissolveMAT.SetFloat("_DissolveAmount", 0f);
	}

	private void Update()
	{
		stateMachine.CurrentState.FrameUpdate();
	}

	private void FixedUpdate()
	{
		stateMachine.CurrentState.PhysicsUpdate();
	}
	#endregion

	public void OnEnemyBulletHit(float damge)
	{
		if (stateMachine.CurrentState == initState || stateMachine.CurrentState == dieState || !IsEnemyInteractable) return;

		currentHealth.CurrentValue = currentHealth.CurrentValue - damge;
		if (currentHealth.CurrentValue <= 0)
		{
			stateMachine.ChangeState(dieState);
			return;
		}
		//StartCoroutine(FlashWhite());
	}

	//private IEnumerator FlashWhite()
	//{
	//	spriteRenderer.material = damageFlashMATRunTime;
	//	damageFlashMATRunTime.SetFloat("_FlashAmount", 0.5f);
	//	yield return new WaitForSeconds(0.05f);
	//	damageFlashMATRunTime.SetFloat("_FlashAmount", 0f);
	//}

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

	public Vector2 AngleToVector2(float angleInDegrees)
	{
		float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
		return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
	}

	public void SpawnArcBullets(Vector2 pos, Vector2 direction, float totalAngle, int bulletCount, FlyweightType type = FlyweightType.LichGunBullet)
	{
		float startAngle = -totalAngle / 2f;
		float stepAngle = totalAngle / (bulletCount - 1);

		for (int i = 0; i < bulletCount; i++)
		{
			float angle = startAngle + i * stepAngle;

			takeBulletFunc.GetFunction()((
				type,
				pos,
				Vector2ToAngle(direction) + angle
			));
		}
	}

	public void SpawnTrail(GameObject trail, float duration)
	{
		StartCoroutine(SpawnTrailCoroutine(trail, duration));
	}

	private IEnumerator SpawnTrailCoroutine(GameObject trail, float duration)
	{
		trail.gameObject.SetActive(true);
		yield return new WaitForSeconds(duration);
		trail.gameObject.SetActive(false);
	}

	public void ClearAllTrail()
	{
		trailCenter.SetActive(false);
		trailLeft.SetActive(false);
		trailRight.SetActive(false);
	}

	public void Dissolve(float duration)
	{
		StartCoroutine(DissolveEffect(duration));
	}

	private IEnumerator DissolveEffect(float duration)
	{
		float elapsedTime = 0f;
		dissolveMAT.SetFloat("_DissolveAmount", 0f);
		spriteRenderer.material = dissolveMAT;
		while (elapsedTime < duration)
		{
			float dissolveAmount = Mathf.Lerp(0f, 1f, elapsedTime / duration);
			dissolveMAT.SetFloat("_DissolveAmount", dissolveAmount);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		dissolveMAT.SetFloat("_DissolveAmount", 1f);
	}
}
