using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapCameraController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
	public Camera miniMapCamera; // Camera của minimap
	public RectTransform miniMapRect; // RectTransform của RawImage hiển thị minimap
	public Vector2Variable playerPos; // Vị trí của player

	private float dragSpeedX; // Tốc độ kéo camera
	private float dragSpeedY; // Tốc độ kéo camera

	private Vector3 dragStartCameraPosition; // Vị trí camera khi bắt đầu kéo
	private Vector2 dragStartMousePosition; // Vị trí chuột khi bắt đầu kéo

	public void Start()
	{
		float cameraHeight = miniMapCamera.orthographicSize * 2;
		float cameraWidth = cameraHeight * (miniMapCamera.pixelWidth / (float)miniMapCamera.pixelHeight);
		Debug.Log("Camera width: " + cameraWidth + " Camera height: " + cameraHeight);
		Debug.Log("MiniMap width: " + miniMapRect.sizeDelta.x + " MiniMap height: " + miniMapRect.sizeDelta.y);
		dragSpeedY = cameraHeight / miniMapRect.sizeDelta.y;
		dragSpeedX = cameraWidth / miniMapRect.sizeDelta.x;
	}

	public void OnEnable()
	{
		// Đặt vị trí camera ban đầu
		miniMapCamera.transform.position = new Vector3(playerPos.CurrentValue.x, playerPos.CurrentValue.y, miniMapCamera.transform.position.z);

	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		// Lưu vị trí camera ban đầu khi bắt đầu kéo
		dragStartCameraPosition = miniMapCamera.transform.position;

		// Lưu vị trí chuột ban đầu (trong không gian màn hình)
		dragStartMousePosition = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		// Tính toán độ lệch chuột (delta) từ vị trí ban đầu
		Vector2 mouseDelta = eventData.position - dragStartMousePosition;

		// Chuyển đổi delta chuột từ không gian màn hình sang world space
		Vector2 worldDelta = new Vector2(
			-mouseDelta.x  * dragSpeedX,
			-mouseDelta.y  * dragSpeedY
		);

		// Cập nhật vị trí camera dựa trên độ lệch world space
		Vector3 newCameraPosition = dragStartCameraPosition + new Vector3(worldDelta.x, worldDelta.y, 0);

		// Cập nhật vị trí camera
		miniMapCamera.transform.position = newCameraPosition;
	}
}