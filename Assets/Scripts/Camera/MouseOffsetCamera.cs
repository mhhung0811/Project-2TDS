using System;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOffsetCamera : MonoBehaviour
{
	public Vector2Variable playerPos;
	public float followRatioX = 0.2f;
	public float followRatioY = 0.2f;
	public float maxOffsetX = 5f;
	public float maxOffsetY = 5f;
	public float smoothSpeed = 0.1f;
	public float dampingFactor = 0.5f; // Tỉ lệ giảm tốc độ camera so với chuột

	public CinemachineVirtualCamera virtualCamera; // Gắn Virtual Camera
	private CinemachineConfiner2D confiner2D;

	private Vector3 mouseOffset;
	private Vector3 targetOffset;
	private Vector3 smoothVelocity;

	//shake
	public float shakeDuration;
	private float shakeTimer;
	public float shakeStrength;
	private Vector2 shakeOffset = Vector2.zero;
	private Vector2 shakeDirection = Vector2.zero;

	private void Awake()
	{
		confiner2D = virtualCamera.GetComponent<CinemachineConfiner2D>();
	}

	void Update()
	{
		// Lấy vị trí chuột trong Viewport (0-1)
		Vector3 mouseViewportPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		mouseViewportPos.x = Mathf.Clamp((mouseViewportPos.x - 0.5f) * 2f, -1f, 1f);
		mouseViewportPos.y = Mathf.Clamp((mouseViewportPos.y - 0.5f) * 2f, -1f, 1f);

		// Tính toán offset dựa vào chuột
		float offsetX = mouseViewportPos.x * maxOffsetX * followRatioX;
		float offsetY = mouseViewportPos.y * maxOffsetY * followRatioY;

		// Đặt offset mục tiêu
		targetOffset = new Vector3(offsetX, offsetY, -10f);

	}

	public void ShakeCamera()
	{
		Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		shakeDirection = (mousePosInWorld - (Vector3)playerPos.CurrentValue).normalized;
		shakeOffset = shakeOffset + shakeDirection * shakeStrength;
		shakeTimer = shakeDuration;
	}

	void LateUpdate()
	{
		// Giảm tốc độ camera theo dampingFactor
		mouseOffset = Vector3.Lerp(mouseOffset, targetOffset, dampingFactor * Time.deltaTime);

		Vector2 shakeOffsetCur = Vector2.zero;

		if(shakeTimer > 0)
		{
			shakeTimer -= Time.deltaTime;
			shakeOffsetCur = shakeOffset * Mathf.Sin(shakeTimer/ shakeDuration * Mathf.PI);
		}
		else
		{
			shakeOffset = Vector2.zero;
			shakeDirection = Vector2.zero;
		}

		Vector3 targetPos = (Vector3)playerPos.CurrentValue + mouseOffset - (Vector3)shakeOffsetCur;

		// Di chuyển camera với hiệu ứng SmoothDamp
		transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref smoothVelocity, smoothSpeed);
	}

	public void OffVirtualCamera()
	{
		virtualCamera.enabled = false;
		SoundManager.Instance.StopMusic();
		StartCoroutine(OnVirtualCamera());
	}

	public IEnumerator OnVirtualCamera()
	{
		yield return new WaitForSeconds(3.5f);
		virtualCamera.enabled = true;
		SoundManager.Instance.PlayMusic();
	}

	// Event listener
	public void ChangeCameraBound(Collider2D coll)
	{
		confiner2D.m_BoundingShape2D = coll;
	}
}
