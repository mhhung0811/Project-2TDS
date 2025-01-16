using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RenderMinimap : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Collision detected");
		// Check layer is Minimap

		TilemapRenderer tilemapRenderer = collision.GetComponent<TilemapRenderer>();
		if (tilemapRenderer != null)
		{
			tilemapRenderer.enabled = true;
		}
	}
}
