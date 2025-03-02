using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapClick : MonoBehaviour
{
	public Camera minimapCamera;  // Camera của minimap
	public Transform player;      // Nhân vật sẽ dịch chuyển
	public RectTransform minimapUI; // UI của minimap (Image hoặc RawImage)

	public float offsetX;
	public float offsetY;

	private void Start()
	{
		float cameraHeight = minimapCamera.orthographicSize * 2;
		float cameraWidth = cameraHeight * (minimapCamera.pixelWidth / (float)minimapCamera.pixelHeight);
		offsetX = cameraWidth / minimapUI.sizeDelta.x;
		offsetY = cameraHeight / minimapUI.sizeDelta.y;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0)) // Kiểm tra click chuột trái
		{
			Vector2 mousePos = Input.mousePosition;

			// Kiểm tra xem chuột có nằm trong khu vực minimap không
			if (RectTransformUtility.RectangleContainsScreenPoint(minimapUI, mousePos))
			{
				Debug.Log("Click on minimap");
				TeleportPlayer(mousePos);
			}
		}
	}

	void TeleportPlayer(Vector2 screenPos)
	{
		// Chuyển đổi tọa độ minimap sang tọa độ thế giới
		Vector2 pointInWorld = ConvertPointInMinimapToWorldPoint();

		// Bắn Ray từ trên xuống (trong 2D, bắn theo Vector2.down là sai)
		RaycastHit2D hit = Physics2D.Raycast(pointInWorld, Vector2.zero);

		if (hit.collider != null) // Nếu có va chạm với Collider 2D
		{
			Debug.Log("Hit point: " + hit.point);
			player.position = hit.collider.transform.position;
		}
		else
		{
			Debug.Log("Raycast không chạm vào gì cả!");
		}
	}


	public Vector2 ConvertPointInMinimapToWorldPoint()
	{
		Vector2 mouseDelta = Input.mousePosition - minimapUI.position;

		Vector2 worldDelta = new Vector2(
			mouseDelta.x * offsetX,
			mouseDelta.y * offsetY
		);

		Vector2 pointInWorld = new Vector2(
			minimapCamera.transform.position.x + worldDelta.x,
			minimapCamera.transform.position.y + worldDelta.y
		);

		Debug.Log("Point in world: " + pointInWorld);

		return pointInWorld;
	}
}
