using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortGun : GunBase
{
    public GunDataSO gunDataSO;
	public FactorySpawnEvent factoryDespawnEvent;
	
	private Coroutine reloadingCoroutine;
	
	public int bulletCount = 5;
	public float spreadAngle = 10;

	public override void InitGunData()
	{
		gunName = gunDataSO.gunName;
		maxAmmoPerMag = gunDataSO.maxAmmoPerMag;
		currentAmmo = gunDataSO.maxAmmoPerMag;
		totalAmmo = gunDataSO.totalAmmo;
		reloadTime = gunDataSO.reloadTime;
		fireRate = gunDataSO.fireRate;
		damage = gunDataSO.damage;
		bulletSpeed = gunDataSO.bulletSpeed;
	}

	public override bool CanShoot()
	{
		if (currentAmmo == 0 && totalAmmo == 0)
		{
			StateMachine.ChangeState(OutOfAmmoState);
			return false;
		}
		
		if (StateMachine.CurrentState == ReloadState && currentAmmo > 0)
		{
			return true;
		}

		if (StateMachine.CurrentState == ReloadState && currentAmmo <= 0)
		{
			return false;
		}
		
		if(currentAmmo <= 0)
		{
			reloadingCoroutine = StartCoroutine(Reloading());
			return false;
		}
		
		return (Time.time > (lastShootTime + 1f/fireRate)) && currentAmmo > 0;
	}

	public override void Shoot(float angle)
	{
		if (!CanShoot()) return;

		float totalSpreadAngle = spreadAngle * (bulletCount - 1);
		float startAngle = angle - totalSpreadAngle / 2;
		for (int i = 0; i < bulletCount; i++)
		{
			float currentAngle = startAngle + i * spreadAngle;
			factoryDespawnEvent.Raise(FlyweightType.BasicBullet, this.transform.position, currentAngle);
		}

		if (reloadingCoroutine != null)
		{
			StopCoroutine(reloadingCoroutine);
		}
		
		currentAmmo--;
		UpdateLastShootTime();
		StateMachine.ChangeState(ShootingState);
	}

	public override void SetAmmoWhenReload()
	{
		
	}

	public IEnumerator Reloading()
	{
		for (int i = 0; i < this.maxAmmoPerMag; i++)
		{
			StateMachine.ChangeState(ReloadState);
			yield return new WaitForSeconds(this.reloadTime + 0.05f);
			if (totalAmmo > 0)
			{
				totalAmmo--;
				currentAmmo++;
			}
		}
	}
}
