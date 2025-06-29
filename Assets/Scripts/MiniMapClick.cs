using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapClick : MonoBehaviour
{
	private Camera minimapCamera;  // Camera của minimap
	public RectTransform minimapUI; // UI của minimap (Image hoặc RawImage)
	public RectTransform minimapPanel; // RectTransform của RawImage hiển thị minimap
	public LayerMask minimapLayer; // Layer của các Collider 2D trên minimap
	public Vector2Event teleportEvent; // Sự kiện teleport

	public bool isDragging = false;
	private Vector2 mouseStartPos;
	private float dragThreshold = 5f;
	private float offsetX;
	private float offsetY;

	private void Awake()
	{
		minimapCamera = GameObject.FindWithTag("MenuMiniMapCamera").GetComponent<Camera>();
	}

	private void Start()
	{
		float cameraHeight = minimapCamera.orthographicSize * 2;
		float cameraWidth = cameraHeight * (minimapCamera.pixelWidth / (float)minimapCamera.pixelHeight);
		offsetX = cameraWidth / minimapUI.sizeDelta.x;
		offsetY = cameraHeight / minimapUI.sizeDelta.y;
	}

	void Update()
	{

		if(minimapPanel.gameObject.activeSelf == false)
		{
			return;
		}

		if(Input.GetMouseButtonDown(0)) // Kiểm tra click chuột trái
		{
			mouseStartPos = Input.mousePosition;
			isDragging = false;
		}


		if (Input.GetMouseButton(0)) // Khi đang giữ chuột
		{
			if (Vector2.Distance(mouseStartPos, Input.mousePosition) > dragThreshold)
			{
				isDragging = true; // Nếu di chuyển vượt quá ngưỡng, coi như đang drag
			}
		}

		if (Input.GetMouseButtonUp(0)) // Kiểm tra click chuột trái
		{
			Vector2 mousePos = Input.mousePosition;

			// Kiểm tra xem chuột có nằm trong khu vực minimap không
			if (RectTransformUtility.RectangleContainsScreenPoint(minimapUI, mousePos))
			{
				if (!isDragging) // Chỉ teleport nếu không phải drag
				{
					TeleportPlayer(mousePos);
				}
			}
		}
	}

	void TeleportPlayer(Vector2 screenPos)
	{
		// Chuyển đổi tọa độ minimap sang tọa độ thế giới
		Vector2 pointInWorld = ConvertPointInMinimapToWorldPoint();

		// Bắn Ray từ trên xuống (trong 2D, bắn theo Vector2.down là sai)
		Collider2D[] hits = Physics2D.OverlapPointAll(pointInWorld, minimapLayer);
		foreach (Collider2D hit in hits)
		{
			if (hit.CompareTag("Teleport"))
			{
				Vector2 playerPos = hit.transform.parent.position;
				teleportEvent.Raise(playerPos); // Gọi sự kiện teleport
				break;
			}
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
