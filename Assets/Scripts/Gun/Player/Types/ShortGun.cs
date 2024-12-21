using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortGun : GunBase
{
    public GunDataSO gunDataSO;
	public FactorySpawnEvent factoryDespawnEvent;
	
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

		currentAmmo--;
		UpdateLastShootTime();
		StateMachine.ChangeState(ShootingState);
	}
}
