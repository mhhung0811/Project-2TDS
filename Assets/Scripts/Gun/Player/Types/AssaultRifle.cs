using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : GunBase
{
	public FlyweightTypeVector2FloatEvent takeBulletEvent;
	public VoidEvent playerShootEvent;

	private float offsetX;
	private float offsetY;

	private float posSpawnX;
	private float posSpawnY;

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

	private void SetOffset(float angle)
	{
		angle = (angle + 360) % 360;

		float radian = angle * Mathf.Deg2Rad;
		offsetX = Mathf.Cos(radian) * 0.6f;
		offsetY = Mathf.Sin(radian) * 0.6f;

		if(angle >= 90 && angle <= 270)
		{
			angle -= 90;
		}
		else
		{
			angle += 90;
		}

		radian = angle * Mathf.Deg2Rad;
		Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized * 0.1f;
		offsetX += direction.x;
		offsetY += direction.y;
	}
}
