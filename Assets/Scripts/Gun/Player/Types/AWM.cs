using UnityEngine;

public class AWM : GunBase
{
	public GunData gunData;
	public FlyweightTypeVector2FloatEvent takeBulletEvent;

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


	public override void Shoot(float angle)
	{
		if (!CanShoot()) return;

		takeBulletEvent.Raise((FlyweightType.BasicBullet, new Vector2(this.transform.position.x, this.transform.position.y), angle));

		currentAmmo--;
		UpdateLastShootTime();
		StateMachine.ChangeState(ShootingState);
	}
}
