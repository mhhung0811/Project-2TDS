using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputUtils 
{
    public static Vector2 GetMouseWorldPosition(Camera camera)
    {
		Vector2 mousePosition = Mouse.current.position.ReadValue();

		Vector2 worldPosition = camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, camera.nearClipPlane));

		return worldPosition;
	}
}
