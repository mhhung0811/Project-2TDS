using System.Collections;
using UnityEngine;

public class ShotGun : GunBase
{
	public FlyweightTypeVector2FloatEvent takeBulletEvent;
	public VoidEvent playerShootEvent;

	private Coroutine reloadingCoroutine;
	
	public int bulletCount = 5;
	public float spreadAngle = 10;

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
			takeBulletEvent.Raise((FlyweightType.BasicBullet, new Vector2(this.transform.position.x, this.transform.position.y), currentAngle));
		}

		if (reloadingCoroutine != null)
		{
			StopCoroutine(reloadingCoroutine);
		}
		this.StopAllCoroutines();

		currentAmmo--;
		UpdateLastShootTime();
		StateMachine.ChangeState(ShootingState);
		
		playerShootEvent.Raise(new Void());
		playerAmmo.CurrentValue = currentAmmo;
	}

	public override void SetAmmoWhenReload()
	{
		
	}

	public override void Reload()
	{
		if(StateMachine.CurrentState == ReloadState) return;

		Debug.Log($"Current ammo: {currentAmmo}, Total ammo: {totalAmmo}");
		if (totalAmmo == 0)
		{
			StateMachine.ChangeState(OutOfAmmoState);
			return;
		}

		if (currentAmmo >= maxAmmoPerMag)
		{
			return;
		}

		reloadingCoroutine = StartCoroutine(Reloading());
	}

	public IEnumerator Reloading()
	{
		for (int i = currentAmmo; i < maxAmmoPerMag; i++)
		{

			StateMachine.ChangeState(ReloadState);
			yield return new WaitForSeconds(this.reloadTime + 0.05f);
			if (totalAmmo > 0)
			{
				totalAmmo--;
				currentAmmo++;
				
				playerAmmo.CurrentValue = currentAmmo;
				playerTotalAmmo.CurrentValue = totalAmmo;
			}
		}
	}
}
