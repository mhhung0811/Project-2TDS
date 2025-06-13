using UnityEngine;

public class AWM : GunBase
{
	public GameObjectFlyweightTypeVector2FloatFuncProvider takeBulletFunc;
	public VoidEvent playerShootEvent;

	public override void Shoot(float angle)
	{
		if (!CanShoot()) return;

		takeBulletFunc.GetFunction()((FlyweightType.SniperBullet, new Vector2(this.transform.position.x, this.transform.position.y), angle));

		currentAmmo--;
		UpdateLastShootTime();
		StateMachine.ChangeState(ShootingState);

		playerShootEvent.Raise(new Void());
		playerAmmo.CurrentValue = currentAmmo;
	}
}
