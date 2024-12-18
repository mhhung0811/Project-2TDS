using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour, GunData
{
	// GunData
	public string gunName { get; set; }
	public int maxAmmoPerMag { get; set; }
	public int currentAmmo { get; set; }
	public int totalAmmo { get; set; }
	public float reloadTime { get; set; }
	public float fireRate { get; set; }
	public float damage { get; set; }
	public float bulletSpeed { get; set; }

	// GunStateMachine
	public GunStateMachine StateMachine;
    public GunIdleState IdleState;
	public GunShootingState ShootState;
	public GunReloadingState ReloadState;

	public float lastShootTime;

	private void Awake()
	{
		StateMachine = new GunStateMachine();
		IdleState = new GunIdleState(this, StateMachine);
		ShootState = new GunShootingState(this, StateMachine);
		ReloadState = new GunReloadingState(this, StateMachine);
	}

	void Start()
    {
		InitGunData();
		lastShootTime = -5;
		StateMachine.Initialize(IdleState);
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
		gunName = "GunBase";
		maxAmmoPerMag = 30;
		currentAmmo = 30;
		totalAmmo = 150;
		reloadTime = 3;
		fireRate = 4;
		damage = 10;
		bulletSpeed = 100;
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
		if(StateMachine.CurrentState == ReloadState)
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
}
