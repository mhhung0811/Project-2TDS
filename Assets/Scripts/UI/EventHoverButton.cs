using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public TextMeshProUGUI buttonText;

	public void OnPointerEnter(PointerEventData eventData)
	{
		buttonText.fontSize += 5; // Tăng kích thước text
		buttonText.color = Color.white;
		buttonText.fontWeight = FontWeight.Bold; // Đổi font chữ
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		buttonText.fontSize -= 5; // Trở lại kích thước ban đầu
		buttonText.color = new Color32(185, 185, 185, 255);
	}
}