using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWM : GunBase
{
	public GunDataSO gunData;
	public FactorySpawnEvent factoryDespawnEvent;

	public override void InitGunData()
	{
		gunName = gunData.gunName;
		maxAmmoPerMag = gunData.maxAmmoPerMag;
		currentAmmo = gunData.currentAmmo;
		totalAmmo = gunData.totalAmmo;
		reloadTime = gunData.reloadTime;
		fireRate = gunData.fireRate;
		damage = gunData.damage;
		bulletSpeed = gunData.bulletSpeed;
	}


	public override void Shoot(float angle)
	{
		if (!CanShoot()) return;

		factoryDespawnEvent.Raise(FlyweightType.BasicBullet, this.transform.position, angle);

		currentAmmo--;
		UpdateLastShootTime();
		StateMachine.ChangeState(ShootingState);
	}
}
