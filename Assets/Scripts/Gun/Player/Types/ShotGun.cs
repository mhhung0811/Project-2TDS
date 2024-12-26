using System.Collections;
using UnityEngine;

public class ShotGun : GunBase
{
    public GunData gunData;
	public FactorySpawnEvent factoryDespawnEvent;
	
	private Coroutine reloadingCoroutine;
	
	public int bulletCount = 5;
	public float spreadAngle = 10;

	public override void InitGunData()
	{
		gunName = gunData.gunName;
		maxAmmoPerMag = gunData.maxAmmoPerMag;
		currentAmmo = gunData.maxAmmoPerMag;
		totalAmmo = gunData.totalAmmo;
		reloadTime = gunData.reloadTime;
		fireRate = gunData.fireRate;
		damage = gunData.damage;
		bulletSpeed = gunData.bulletSpeed;
	}

	public override void ResetGunData()
	{
		gunName = gunData.gunName;
		maxAmmoPerMag = gunData.maxAmmoPerMag;
		currentAmmo = gunData.maxAmmoPerMag;
		totalAmmo = gunData.totalAmmo;
		reloadTime = gunData.reloadTime;
		fireRate = gunData.fireRate;
		damage = gunData.damage;
		bulletSpeed = gunData.bulletSpeed;
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
