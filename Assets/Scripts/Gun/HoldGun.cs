﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldGun : MonoBehaviour
{
	[NonSerialized]
	private Camera MainCamera;
	private Player Player;
	public Vector2Variable HoldGunPos;

	private void Awake()
	{
		Player = GetComponentInParent<Player>();
		HoldGunPos.OnChanged += SetPositionWhenChangeGun;
	}

	private void Start()
	{
		// Lấy camera chính
		MainCamera = Camera.main;
	}
	private void Update()
	{
		UpdateRotation();
	}
	
	private void OnDestroy()
	{
		HoldGunPos.OnChanged -= SetPositionWhenChangeGun;
	}

	public void SetPositionWhenChangeGun(Vector2 newPos)
	{
		Debug.Log("------------SetPositionWhenChangeGun");
		transform.localPosition = newPos;
	}


	public void SetActive(bool isActive)
	{
		this.gameObject.SetActive(isActive);
	}


	public void UpdateRotation()
	{
		Vector2 mousePosition = Mouse.current.position.ReadValue();

		Vector2 worldPosition = MainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, MainCamera.nearClipPlane));

		float angle = Vector2ToAngle(worldPosition - new Vector2(transform.position.x, transform.position.y));

		if (!Player.IsFacingRight)
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

		if(angleInDegrees < 0)
		{
			angleInDegrees += 360f;
		}

		return angleInDegrees;
	}
}
