using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IPlayerInteractable, IExplodedInteractable
{
    public IntVariable HP;
	public IntVariable MaxHP;
	public IntVariable Mana;
	public IntVariable MaxMana;

	public Vector2Variable PlayerPos;
    public VoidEvent PlayerHit;
	public VoidEvent PlayerDied;
	public Animator Animator;
    public Rigidbody2D myRb;

    public bool IsPlayerInteractable { get; set; }
	public bool IsExplodedInteractable { get; set; }

    public bool isInvulnerable = false;
	public float invulnerableDuration = 1f;
    public float blinkInterval = 0.1f;

	public float MovementSpeed = 5f;
    public float RollSpeed;
    public float RollDuration;

	[NonSerialized]
	public SpriteRenderer SpriteRenderer;
	[NonSerialized]
	public bool IsFacingRight = true;
	[NonSerialized]
	public bool IsRolling = false;
	[NonSerialized]
	public bool IsPressWASD = false;
	[NonSerialized]
	public bool IsPressShoot = false;
	[NonSerialized]
	public HoldGun HoldGun;
	[NonSerialized]
	public Vector2 MovementInput;

	// private PlayerInventory _inventory;
	private PlayerArsenal _arsenal;
	private Camera MainCamera;

	[Header("Interaction Zone")]
	public float interactionOffSet = 0.25f;
	public CircleCollider2D interactCollider;

	#region State Machine Variables
	public PlayerStateMachine StateMachine;
    public PlayerIdleState IdleState;
    public PlayerMoveState MoveState;
    public PlayerRollState RollState;
    public PlayerDieState DieState;
	#endregion
	private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        RollState = new PlayerRollState(this, StateMachine);
        IdleState = new PlayerIdleState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
		DieState = new PlayerDieState(this, StateMachine);
	}
    void Start()
    {
		// _inventory = GetComponent<PlayerInventory>();
		MainCamera = Camera.main;
		_arsenal = GetComponent<PlayerArsenal>();
        HoldGun = GetComponentInChildren<HoldGun>();
		SpriteRenderer = GetComponent<SpriteRenderer>();
		IsFacingRight = true;
		MovementInput = new Vector2(1, 0);
        StateMachine.Initialize(IdleState);
		isInvulnerable = false;
		IsPlayerInteractable = true;
		IsExplodedInteractable = true;

		if (SaveGameManager.Instance.isGameLoaded)
		{
			HP.CurrentValue = SaveGameManager.Instance.gameData.health;
			Mana.CurrentValue = SaveGameManager.Instance.gameData.mana;
			MaxHP.CurrentValue = SaveGameManager.Instance.gameData.maxHealth;
			MaxMana.CurrentValue = SaveGameManager.Instance.gameData.maxMana;
			this.transform.position = (Vector3)SaveGameManager.Instance.gameData.LastSpawn;
		}
	}

    void Update()
    {
        if (StateMachine.CurrentState == DieState)
        {
            return;
        }

        PlayerPos.CurrentValue = transform.position;
		StateMachine.CurrentState.FrameUpdate();
		UpdateInteractColliderByPosMouse();
        OnShoot();
	}
    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void InputShoot(InputAction.CallbackContext context)
    {
		if (GameManager.Instance.isHoldButtonTab) {
			IsPressShoot = false;
			return;
		}

		if(GameManager.Instance.isOpenUI)
		{
			return;
		}

		if (context.performed && GameManager.Instance.isGamePaused == false)
        {
            IsPressShoot = true;
		}
		else if (context.canceled)
		{
			IsPressShoot = false;
		}

	}
    public void OnShoot()
    {
        if (!IsPressShoot) return;
        if(IsRolling) return;

		Vector2 mousePosition = Mouse.current.position.ReadValue();

		Vector2 worldPosition = MainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, MainCamera.nearClipPlane));

		float angle = Vector2ToAngle(worldPosition - new Vector2(transform.position.x, transform.position.y));

		// _inventory.GetHoldingGun().Shoot(angle);
		_arsenal.GetHoldingGun().Shoot(angle);
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

    public void InputMove(InputAction.CallbackContext context)
    {
		if (GameManager.Instance.isOpenUI)
		{
			return;
		}

		if (context.performed && GameManager.Instance.isGamePaused == false)
        {
            IsPressWASD = true;
            MovementInput = context.ReadValue<Vector2>();

            if(IsRolling)
            {
                return;
            }

            if (MovementInput != Vector2.zero && StateMachine.CurrentState != MoveState)
            {
                StateMachine.ChangeState(MoveState); 
            }
        }
        else if (context.canceled)
        {
            IsPressWASD = false;
            if (IsRolling)
            {
                return;
            }
            StateMachine.ChangeState(IdleState);
        }
    }

    public void Move()
    {
        myRb.velocity = MovementInput * MovementSpeed;
    }

    public void Flip()
    {
        IsFacingRight = !IsFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void InputRoll(InputAction.CallbackContext context)
    {
		if (GameManager.Instance.isOpenUI)
		{
			return;
		}

		if (context.performed && !IsRolling && IsPressWASD && GameManager.Instance.isGamePaused == false)
        {
            StateMachine.ChangeState(RollState);
        }
    }
    
    public void InputInteract(InputAction.CallbackContext context)
    {
		if (GameManager.Instance.isOpenUI)
		{
			return;
		}

		if (context.performed && GameManager.Instance.isGamePaused == false)
	    {
		    Interact();
	    }
    }
    
    public void InputReload(InputAction.CallbackContext context)
	{
		if (GameManager.Instance.isOpenUI)
		{
			return;
		}

		if (context.performed && GameManager.Instance.isGamePaused == false)
	    {
		    // _inventory.GetHoldingGun().Reload();
		    _arsenal.GetHoldingGun().Reload();
	    }
	}

    public void Interact()
    {
	    interactCollider.GetComponent<InteractionZone>().Interact(this.gameObject);
    }
    
    public void InputSwapGun1(InputAction.CallbackContext context)
	{
		if (GameManager.Instance.isOpenUI)
		{
			return;
		}

		if (context.performed && GameManager.Instance.isGamePaused == false)
	    {
		    SwapGun(0);
	    }
	}
	public void InputSwapGun2(InputAction.CallbackContext context)
	{
		if (GameManager.Instance.isOpenUI)
		{
			return;
		}
		if (context.performed && GameManager.Instance.isGamePaused == false)
		{
			SwapGun(1);
		}
	}
	public void InputSwapGun3(InputAction.CallbackContext context)
	{
		if (GameManager.Instance.isOpenUI)
		{
			return;
		}
		if (context.performed && GameManager.Instance.isGamePaused == false)
		{
			SwapGun(2);
		}
	}
	
	public void SwapGun(int index)
	{
		// _inventory.SwitchGun(index);
		_arsenal.SwitchGun(index);
	}
	
	// Get position of mouse
	public Vector2 GetMousePosition()
	{
		Vector2 mousePosition = Mouse.current.position.ReadValue();
		Vector2 worldPosition = MainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, MainCamera.nearClipPlane));
		return worldPosition;
	}

	

	public void UpdateAnimationByPosMouse()
    {
        Vector2 mousePosition = GetMousePosition();
        Vector2 direction = mousePosition - new Vector2(transform.position.x, transform.position.y);
        Animator.SetFloat("XInput", direction.x);
		Animator.SetFloat("YInput", direction.y);

        float angle = Vector2ToAngle(direction);
        if(angle > 22.5f && angle < 157.5)
        {
            SpriteRenderer.sortingOrder = 2;
        }
        else
		{
			SpriteRenderer.sortingOrder = 0;
		}

		// Check Flip
		if (direction.x > 0 && !IsFacingRight)
		{
			Flip();
		}
		else if (direction.x < 0 && IsFacingRight)
		{
			Flip();
		}
	}
    
    public void UpdateInteractColliderByPosMouse()
    {
		// Get the mouse position in world coordinates
		Vector2 mousePosition = GetMousePosition();

		// Calculate the angle between the player and the mouse
		Vector2 direction = mousePosition - (Vector2)transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x); // Angle in radians

		Vector2 newColliderPosition = new Vector2(
			transform.position.x + Mathf.Cos(angle) * interactionOffSet,
			transform.position.y + Mathf.Sin(angle) * interactionOffSet
		);
		// Calculate the new position of the collider using trigonometry

		// Update the collider's position
		interactCollider.transform.position = newColliderPosition;
	}

	public void Die()
	{
		StateMachine.ChangeState(DieState);
		HoldGun.SetActive(false);
	}

	// Event Listener
	public void Teleport(Vector2 pos)
	{
		this.transform.position = pos;
	}

	public void OnPlayerBulletHit()
	{
        if (isInvulnerable) return;

        HP.CurrentValue = HP.CurrentValue - 1;
		SaveGameManager.Instance.gameData.health = HP.CurrentValue;
		SaveGameManager.Instance.SaveGame(SaveGameManager.Instance.gameData);

		if (HP.CurrentValue <= 0)
        {
            Die();
            return;
        }
        StartCoroutine(InvulnerablilityCoroutine());

		// Camera shake
        var @void = new Void();
		PlayerHit.Raise(@void);
	}

    public void OnExplode()
	{
		if (isInvulnerable) return;

		HP.CurrentValue = HP.CurrentValue - 1;

		if (HP.CurrentValue <= 0)
		{
			Die();
			return;
		}
		StopAllCoroutines();
		StartCoroutine(InvulnerablilityCoroutine());
		StartCoroutine(Exploded());

		// Camera shake
		var @void = new Void();
		PlayerHit.Raise(@void);
	}

	public IEnumerator Exploded()
	{
		PlayerInput playerInput = GetComponent<PlayerInput>();
		playerInput.enabled = false;
		yield return new WaitForSeconds(1f);
		playerInput.enabled = true;
		myRb.drag = 0;
	}

	public void OnRefillMana()
	{
		if(HP.CurrentValue > 1)
		{
			HP.CurrentValue = HP.CurrentValue - 1;
			StartCoroutine(RefillMana());
		}
	}

	private IEnumerator RefillMana()
	{
		float timeToWait = 0.5f;
		float elapsedTime = 0f;
		while (elapsedTime < timeToWait)
		{
			elapsedTime += Time.deltaTime;
			Mana.CurrentValue = (int)Mathf.Lerp(0, MaxMana.CurrentValue, elapsedTime / timeToWait);
			yield return null;
		}
	}

	private IEnumerator InvulnerablilityCoroutine()
    {
		isInvulnerable = true;
		IsPlayerInteractable = false;

		float elapsedTime = 0f;
		while (elapsedTime < invulnerableDuration)
		{
			SpriteRenderer.enabled = !SpriteRenderer.enabled;

			yield return new WaitForSeconds(blinkInterval);
			elapsedTime += blinkInterval;
		}

		SpriteRenderer.enabled = true;

		isInvulnerable = false;
		IsPlayerInteractable = true;
	}

	public void OnCamUnfocus()
	{
		PlayerInput playerInput = GetComponent<PlayerInput>();
		playerInput.enabled = false;
		StartCoroutine(ActivatePlayerInput());
	}
	
	public IEnumerator ActivatePlayerInput()
	{
		yield return new WaitForSeconds(3f);
		PlayerInput playerInput = GetComponent<PlayerInput>();
		playerInput.enabled = true;
	}

	#region Animation Triggers
	public void AnimationTriggerEvent(AnimationTriggerType triggerEvent)
    {
        StateMachine.CurrentState.AnimationTriggerEvent(triggerEvent);
    }
    public enum AnimationTriggerType
    {
        RollEnd,
    }
    #endregion
}
