using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOffsetCamera : MonoBehaviour
{
	public Transform player;
	public float followRatioX = 0.2f;
	public float followRatioY = 0.2f;
	public float maxOffsetX = 5f;
	public float maxOffsetY = 5f;
	public float smoothSpeed = 0.1f;
	public float dampingFactor = 0.5f; // Tỉ lệ giảm tốc độ camera so với chuột

	private Vector3 mouseOffset;
	private Vector3 targetOffset;
	private Vector3 smoothVelocity;

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

	void LateUpdate()
	{
		// Lấy vị trí mục tiêu của camera
		Vector3 targetPosition = player.position + targetOffset;

		// Giảm tốc độ camera theo dampingFactor
		mouseOffset = Vector3.Lerp(mouseOffset, targetOffset, dampingFactor * Time.deltaTime);

		// Di chuyển camera với hiệu ứng SmoothDamp
		transform.position = Vector3.SmoothDamp(transform.position, player.position + mouseOffset, ref smoothVelocity, smoothSpeed);
	}
}
