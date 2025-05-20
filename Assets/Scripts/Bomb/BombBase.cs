using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BombBase : MonoBehaviour
{
	public float gravity = -9.81f;
	public float radius;
	protected float verticalSpeed;
	protected float horizontalSpeed;
	protected float height;
	protected Vector2 basePos;
	protected Vector2 direction;
	protected bool isThrowing = false;


	public virtual void Throwing()
	{
		verticalSpeed = verticalSpeed + gravity * Time.deltaTime;
		height = height + verticalSpeed * Time.deltaTime;

		basePos += direction * horizontalSpeed * Time.deltaTime;

		this.transform.position = new Vector3(basePos.x, basePos.y + height, 0.0f);

		if (height < 0.0f)
		{
			isThrowing = false;
			height = 0.0f;
			basePos = this.transform.position;
			Explode();
		}
	}
	public virtual void Throw(Vector2 pos) { }
	public virtual void Explode() { }
}
