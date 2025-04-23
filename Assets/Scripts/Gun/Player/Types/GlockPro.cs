using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlockPro : GunBase
{
	public FlyweightTypeVector2FloatEvent takeBulletEvent;
	public VoidEvent playerShootEvent;

	public override void Shoot(float angle)
	{
		if (!CanShoot()) return;

		takeBulletEvent.Raise((FlyweightType.GlockProBullet, new Vector2(this.transform.position.x, this.transform.position.y + 0.25f), angle));

		currentAmmo--;
		ConsumeMana();
		UpdateLastShootTime();
		StateMachine.ChangeState(ShootingState);

		playerShootEvent.Raise(new Void());
		playerAmmo.CurrentValue = currentAmmo;
	}
}
