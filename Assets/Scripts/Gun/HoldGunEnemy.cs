using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldGunEnemy : MonoBehaviour
{
	public Vector2Variable playerPos;
	private void Start()
	{

	}
	private void Update()
	{
		UpdateRotation();
	}

	public void SetActive(bool isActive)
	{
		this.gameObject.SetActive(isActive);
	}

	public void UpdateRotation()
	{
		Vector2 direction = playerPos.CurrentValue - new Vector2(transform.position.x, transform.position.y);
		float angle = Vector2ToAngle(direction);

		bool isFacingRight = Math.Abs(transform.parent.eulerAngles.y - 180.0f) > 1f;

		if (!isFacingRight)
		{
			transform.localScale = new Vector3(1, -1, 1);
		}
		else
		{
			transform.localScale = new Vector3(1, 1, 1);
		}

		transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	public float Vector2ToAngle(Vector2 direction)
	{
		float angleInRadians = Mathf.Atan2(direction.y, direction.x);

		float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

		if (angleInDegrees < 0)
		{
			angleInDegrees += 360f;
		}

		return angleInDegrees;
	}
}
