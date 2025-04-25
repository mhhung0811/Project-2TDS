using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : GunBase
{
	public FlyweightTypeVector2FloatEvent takeBulletEvent;
	public VoidEvent playerShootEvent;

	public override void Shoot(float angle)
	{
		if (!CanShoot()) return;

		SetOffset(angle);

		takeBulletEvent.Raise((FlyweightType.AssaultRifleBullet, new Vector2(this.transform.position.x + offsetX, this.transform.position.y + offsetY), angle));

		currentAmmo--;
		UpdateLastShootTime();
		StateMachine.ChangeState(ShootingState);
		//SoundManager.Instance.PlaySound("Ak47Shot");

		playerShootEvent.Raise(new Void());
		playerAmmo.CurrentValue = currentAmmo;
	}
}
