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

		takeBulletEvent.Raise((FlyweightType.AssaultRifleBullet, new Vector2(this.transform.position.x, this.transform.position.y), angle));

		currentAmmo--;
		UpdateLastShootTime();
		StateMachine.ChangeState(ShootingState);

		playerShootEvent.Raise(new Void());
	}
}
