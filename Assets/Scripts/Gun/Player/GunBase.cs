using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour, IGunData
{
	public int gunId { get; private set; }

	// GunData
	public string gunName { get; set; }
	public int maxAmmoPerMag { get; set; }
	public int currentAmmo { get; set; }
	public int totalAmmo { get; set; }
	public float reloadTime { get; set; }
	public float fireRate { get; set; }
	public Vector3 posHoldGun { get; set; }
	public Vector3 posGun { get; set; }

	[field: SerializeField] public GunData GunData;

	[field: SerializeField] public Vector2Variable HoldGunPos;

	// GunStateMachine
	public GunStateMachine StateMachine;
    public GunIdleState IdleState;
	public GunShootingState ShootingState;
	public GunReloadingState ReloadState;
	public GunOutOfAmmoState OutOfAmmoState;
	public float lastShootTime { get; private set; }
	public Animator animator { get; private set; }
	public HoldGun holdGun { get; private set; }
	
	public StringBoolEvent changeGunStateEvent;
	public FloatVariable playerMaxReloadTime { get; private set; }
	public FloatVariable playerReloadTime { get; private set; }
	public BoolVariable playerIsReloading { get; private set; }
	public IntVariable playerMaxAmmo { get; private set; }
	public IntVariable playerAmmo { get; private set; }

	private void Awake()
	{
		StateMachine = new GunStateMachine();
		IdleState = new GunIdleState(this, StateMachine);
		ShootingState = new GunShootingState(this, StateMachine);
		ReloadState = new GunReloadingState(this, StateMachine);
		OutOfAmmoState = new GunOutOfAmmoState(this, StateMachine);

		holdGun = GetComponentInParent<HoldGun>();
		InitGunData();
	}

	void Start()
    {
		lastShootTime = -5;
		StateMachine.Initialize(IdleState);
		animator = GetComponent<Animator>();
	}

    void Update()
    {
		StateMachine.CurrentState.FrameUpdate();
	}

	private void FixedUpdate()
	{
		StateMachine.CurrentState.PhysicsUpdate();
	}

	public virtual void InitGunData()
	{
		gunName = GunData.gunName;
		maxAmmoPerMag = GunData.maxAmmoPerMag;
		currentAmmo = GunData.currentAmmo;
		totalAmmo = GunData.totalAmmo;
		reloadTime = GunData.reloadTime;
		fireRate = GunData.fireRate;
		posHoldGun = GunData.posHoldGun;
		posGun = GunData.posGun;

		HoldGunPos.CurrentValue = (Vector2)posHoldGun;

		this.transform.localPosition = GunData.posGun;
	}

	public virtual void ResetGunData()
	{
		gunName = GunData.gunName;
		maxAmmoPerMag = GunData.maxAmmoPerMag;
		currentAmmo = GunData.currentAmmo;
		totalAmmo = GunData.totalAmmo;
		reloadTime = GunData.reloadTime;
		fireRate = GunData.fireRate;
		posHoldGun = GunData.posHoldGun;
		posGun = GunData.posGun;

		Debug.Log("Reset Base Gun Data");
	}

	public virtual void Shoot(float angle)
	{
		
	}

	public void UpdateLastShootTime()
	{
		lastShootTime = Time.time;
	}

	public virtual bool CanShoot()
	{
		if (currentAmmo == 0 && totalAmmo == 0)
		{
			StateMachine.ChangeState(OutOfAmmoState);
			return false;
		}

		if (StateMachine.CurrentState == ReloadState)
		{
			return false;
		}

		if(currentAmmo <= 0)
		{
			StateMachine.ChangeState(ReloadState);
			return false;
		}

		return (Time.time > (lastShootTime + 1f/fireRate)) && currentAmmo > 0;
	}

	public virtual void SetAmmoWhenReload()
	{
		int ammoToReload = maxAmmoPerMag - currentAmmo;
		if(totalAmmo >= ammoToReload)
		{
			totalAmmo -= ammoToReload;
			currentAmmo = maxAmmoPerMag;
		}
		else
		{
			currentAmmo += totalAmmo;
			totalAmmo = 0;
		}
	}
	
	public virtual void Reload()
	{
		// Debug.Log($"Current ammo: {currentAmmo}, Total ammo: {totalAmmo}");
		if (totalAmmo == 0)
		{
			StateMachine.ChangeState(OutOfAmmoState);
			return;
		}

		if (currentAmmo >= maxAmmoPerMag)
		{
			return;
		}

		StateMachine.ChangeState(ReloadState);
	}
	
	public void SetGunId(int id)
	{
		gunId = id;
	}

	#region UI

	public void UpdateReloadActive(bool value)
	{
		playerIsReloading.CurrentValue = value;
	}
	
	public void UpdateReloadTime(float value)
	{
		
		playerReloadTime.CurrentValue = value;
	}
	
	public void SetUpReloadTimeVariables(FloatVariable maxReloadTime, FloatVariable reloadTime, BoolVariable isReloading)
	{
		playerMaxReloadTime = maxReloadTime;
		playerReloadTime = reloadTime;
		playerIsReloading = isReloading;
		
		playerMaxReloadTime.CurrentValue = this.reloadTime;
		playerReloadTime.CurrentValue = 0;
	}
	
	public void ResetReloadTimeVariables()
	{
		playerMaxReloadTime.CurrentValue = 1;
		playerReloadTime.CurrentValue = 0;
		playerIsReloading.CurrentValue = false;
		
		playerMaxReloadTime = null;
		playerReloadTime = null;
		playerIsReloading = null;
	}
	
	public void UpdateMaxAmmo(int value)
	{
		playerMaxAmmo.CurrentValue = value;
	}
	
	public void UpdateAmmo(int value)
	{
		playerAmmo.CurrentValue = value;
	}
	
	public void SetUpAmmoVariables(IntVariable maxAmmo, IntVariable ammo)
	{
		playerMaxAmmo = maxAmmo;
		playerAmmo = ammo;
		
		playerMaxAmmo.CurrentValue = maxAmmoPerMag;
		playerAmmo.CurrentValue = currentAmmo;
	}
	
	public void ResetAmmoVariables()
	{
		playerMaxAmmo.CurrentValue = 0;
		playerAmmo.CurrentValue = 0;
		
		playerMaxAmmo = null;
		playerAmmo = null;
	}

	#endregion
}
